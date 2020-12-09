using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class GpsService
{
    private Action<LocationInfo> _onStarted;
    private TextMeshProUGUI _logText;

    public GpsService(Action<LocationInfo> onStarted, TextMeshProUGUI logText = null)
    {
        _onStarted = onStarted;
        _logText = logText;
    }

    public IEnumerator StartServiceRoutine(bool autoStopAtEnd = true)
    {
        // First, check if user has location service enabled
        if (!Input.location.isEnabledByUser)
        {
            Debug.LogError("Gps isn't enabled by user");
            if (_logText)
                _logText.text = "Gps isn't enabled by user";
            yield break;
        }

        // Start service before querying location
        Input.location.Start();

        // Wait until service initializes
        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // Service didn't initialize in 20 seconds
        if (maxWait < 1)
        {
            Debug.LogError("GpsService: Timed out");
            if (_logText)
                _logText.text = "GpsService: Timed out";
            yield break;
        }

        // Connection has failed
        if (Input.location.status == LocationServiceStatus.Failed)
        {
            Debug.LogError("Unable to determine device location");
            if (_logText)
                _logText.text = "Unable to determine device location";
            yield break;
        }
        else
        {
            // Access granted and location value could be retrieved
            Debug.Log("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
            if (_logText)
                _logText.text = "Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp;
            _onStarted?.Invoke(Input.location.lastData);
        }

        // Stop service if there is no need to query location updates continuously
        if (autoStopAtEnd)
            Input.location.Stop();
    }

    public void StopService()
    {
        Input.location.Stop();
    }

    /// <summary>
    /// Retorna la distance entre dos posiciones (latitud y longitud) en metros
    /// </summary>
    /// <param name="lat1"></param>
    /// <param name="lng1"></param>
    /// <param name="lat2"></param>
    /// <param name="lng2"></param>
    /// <returns></returns>
    public static double Distance(double lat1, double lng1, double lat2, double lng2)
    {

        double earthRadius = 6371; // in kilometer, change to 3958.75 for miles output

        double dLat = (lat2 - lat1) * (Math.PI / 180);
        double dLng = (lng2 - lng1) * (Math.PI / 180);

        double sinLat = Math.Sin(dLat / 2);
        double sinLng = Math.Sin(dLng / 2);

        double a = Math.Pow(sinLat, 2) + Math.Pow(sinLng, 2)
            * Math.Cos(lat1 * (Math.PI / 180)) * Math.Cos(lat2 * (Math.PI / 180));

        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        double dist = earthRadius * c;

        return dist * 1000; // output distance, in METERS
    }
}