using System;

namespace sgc_backend.Controllers
{
    public static class ZonaHoraria
    {
        private static readonly DateTime dateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.Local);
        //public static string ObtenerFechaDePeru() => dateTime.ToShortDateString();      // 9/07/2022
        public static DateTime ObtenerFechaDePeru() => dateTime;      // 9/07/2022
        //public static string ObtenerHoraDePeru() => dateTime.ToLongTimeString();        // 22:09:59
        //public static int ObtenerHoraDePeru() => dateTime.Hour;        // 22:09:59
        //public static string ObtenerSoloMinutoDePeru() => dateTime.Minute.ToString();        // 22:09:59
        //public static string ObtenerSoloSegundoDePeru() => dateTime.Second.ToString();        // 22:09:59
    }
}
