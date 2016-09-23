
namespace GMap.NET
{
   using System;
   using System.Globalization;

   /// <summary>
   /// the point of coordinates
   /// </summary>
   [Serializable]
   public struct PointLatLng
   {
      public static readonly PointLatLng Zero;
      private double lat;
      private double lng;

      public PointLatLng(double lat, double lng)
      {
         this.lat = lat;
         this.lng = lng;
      }

      public bool IsZero
      {
         get
         {
            return ((this.lng == 0d) && (this.lat == 0d));
         }
      }

      public double Lat
      {
         get
         {
            return this.lat;
         }
         set
         {
            this.lat = value;
         }
      }

      public double Lng
      {
         get
         {
            return this.lng;
         }
         set
         {
            this.lng = value;
         }
      }

      public static PointLatLng operator+(PointLatLng pt, SizeLatLng sz)
      {
         return Add(pt, sz);
      }

      public static PointLatLng operator-(PointLatLng pt, SizeLatLng sz)
      {
         return Subtract(pt, sz);
      }

      public static bool operator==(PointLatLng left, PointLatLng right)
      {
         return ((left.Lng == right.Lng) && (left.Lat == right.Lat));
      }

      public static bool operator!=(PointLatLng left, PointLatLng right)
      {
         return !(left == right);
      }

      public static PointLatLng Add(PointLatLng pt, SizeLatLng sz)
      {
         return new PointLatLng(pt.Lat - sz.HeightLat, pt.Lng + sz.WidthLng);
      }

      public static PointLatLng Subtract(PointLatLng pt, SizeLatLng sz)
      {
         return new PointLatLng(pt.Lat + sz.HeightLat, pt.Lng - sz.WidthLng);
      }

      public override bool Equals(object obj)
      {
         if(!(obj is PointLatLng))
         {
            return false;
         }
         PointLatLng tf = (PointLatLng) obj;
         return (((tf.Lng == this.Lng) && (tf.Lat == this.Lat)) && tf.GetType().Equals(base.GetType()));
      }

      public void Offset(PointLatLng pos)
      {
         this.Offset(pos.Lat, pos.Lng);
      }

      public void Offset(double lat, double lng)
      {
         this.Lng += lng;
         this.Lat -= lat;
      }

      public override int GetHashCode()
      {
         return (this.Lng.GetHashCode() ^ this.Lat.GetHashCode());
      }

      public override string ToString()
      {
         return string.Format(CultureInfo.CurrentCulture, "{{Lat={0}, Lng={1}}}", this.Lat, this.Lng);
      }

      static PointLatLng()
      {
         Zero = new PointLatLng();
      }

      public PointLatLng createPointLatLngFromCenterMiles(double DistanceMiles)
      {
          PointLatLng distancePoint = new PointLatLng();

          //xMeter = xMeter / 1000;

          double earthRadius = 3958.0;
          double lat = this.Lat * (Math.PI / 180);
          double lon = this.Lng * (Math.PI / 180);

          double d = DistanceMiles / earthRadius;

          double brng = 360 * (Math.PI / 180);

          var latRadians = Math.Asin(Math.Sin(lat) * Math.Cos(d) + Math.Cos(lat) * Math.Sin(d) * Math.Cos(brng));
          var lngRadians = lon + Math.Atan2(Math.Sin(brng) * Math.Sin(d) * Math.Cos(lat), Math.Cos(d) - Math.Sin(lat) * Math.Sin(latRadians));

          distancePoint.Lat = latRadians * 180 / Math.PI;
          distancePoint.Lng = lngRadians * 180 / Math.PI;

          return distancePoint;
      }

      public PointLatLng createPointLatLngFromCenterKilometers(double DistanceKilometers)
      {
          PointLatLng distancePoint = new PointLatLng();

          //xMeter = xMeter / 1000;

          double earthRadius = 6367.0;
          double lat = this.Lat * (Math.PI / 180);
          double lon = this.Lng * (Math.PI / 180);

          double d = DistanceKilometers / earthRadius;
          double brng = 360 * (Math.PI / 180);

          var latRadians = Math.Asin(Math.Sin(lat) * Math.Cos(d) + Math.Cos(lat) * Math.Sin(d) * Math.Cos(brng));
          var lngRadians = lon + Math.Atan2(Math.Sin(brng) * Math.Sin(d) * Math.Cos(lat), Math.Cos(d) - Math.Sin(lat) * Math.Sin(latRadians));

          distancePoint.Lat = latRadians * 180 / Math.PI;
          distancePoint.Lng = lngRadians * 180 / Math.PI;

          return distancePoint;
      }


   }
}