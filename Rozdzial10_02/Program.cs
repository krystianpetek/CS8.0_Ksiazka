﻿using System;

namespace Rozdzial10_02
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Coordinate coordinate1, coordinate2;
            coordinate1 = new Coordinate(new Longitude(48, 52), new Latitude(-2, -20));
            Console.WriteLine(coordinate1);
            Arc arc = new Arc(new Longitude(3, 5), new Latitude(1));
            coordinate2 = coordinate1 + arc;
            Console.WriteLine(coordinate2);
            coordinate2 = coordinate2 - arc;
            Console.WriteLine(coordinate2);
            coordinate2 += arc;
            Console.WriteLine(coordinate2);
            Console.WriteLine(-coordinate1);
        }
    }

    public struct Latitude
    {
        public Latitude(int x, int y = 0)
        {
            X = x;
            Y = y;
            DecimalDegrees = default;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return X + " " + Y;
        }

        public static Latitude operator -(Latitude latitude)
        {
            return new Latitude(-latitude.X, -latitude.Y);
        }
        public static Latitude operator +(Latitude latitude)
        {
            return latitude;
        }

        // konwersja niejawna miedzy typami latitude i double, zle
        public Latitude(double decimalDegrees)
        {
            X = default;
            Y = default;
            DecimalDegrees = decimalDegrees;
        }
        public double DecimalDegrees { get;}
        public static implicit operator double(Latitude latitude)
        {
            return latitude.DecimalDegrees;
        }
        public static implicit operator Latitude(double degrees)
        {
            return new Latitude(degrees);
        }
    }

    public struct Longitude
    {
        public Longitude(int x, int y = 0)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return X + " " + Y;
        }

        public static Longitude operator -(Longitude longitude)
        {
            return new Longitude(-longitude.X, -longitude.Y);
        }
    }

    public struct Coordinate
    {
        public override string ToString()
        {
            return longitude + " " + latitude;
        }

        public Latitude latitude { get; }
        public Longitude longitude { get; }

        public Coordinate(Longitude x, Latitude y)
        {
            longitude = x;
            latitude = y;
        }

        public static Coordinate operator +(Coordinate source, Arc arc)
        {
            Coordinate result = new Coordinate(new Longitude(source.longitude.X + arc.LongitudeDifference.X, source.longitude.Y + arc.LongitudeDifference.Y), new Latitude(source.latitude.X + arc.LatitudeDifference.X, source.latitude.Y + arc.LatitudeDifference.Y));
            return result;
        }

        public static Coordinate operator -(Coordinate source, Arc arc)
        {
            Coordinate result = new Coordinate(new Longitude(source.longitude.X - arc.LongitudeDifference.X, source.longitude.Y - arc.LongitudeDifference.Y), new Latitude(source.latitude.X - arc.LatitudeDifference.X, source.latitude.Y - arc.LatitudeDifference.Y));
            return result;
        }

        public static Coordinate operator +(Coordinate coordinate)
        {
            return coordinate;
        }

        public static Coordinate operator -(Coordinate coordinate)
        {
            return new Coordinate(new Longitude(-coordinate.longitude.X, -coordinate.longitude.Y), new Latitude(-coordinate.latitude.X, -coordinate.latitude.Y));
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (this.GetType() != obj.GetType())
                return false;

            return Equals((Coordinate)obj);
        }

        public bool Equals(Coordinate? coordinate) => (latitude, longitude).Equals((coordinate?.longitude, coordinate?.latitude));

        public static bool operator ==(Coordinate left, Coordinate right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Coordinate left, Coordinate right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return (longitude.X, longitude.Y, latitude.X, latitude.Y).GetHashCode();
        }
    }

    // dodawanie operatora +
    public struct Arc
    {
        public Arc(Longitude longitudeDifference, Latitude latitudeDifference)
        {
            LongitudeDifference = longitudeDifference;
            LatitudeDifference = latitudeDifference;
        }

        public Longitude LongitudeDifference { get; }
        public Latitude LatitudeDifference { get; }

        public static Arc operator -(Arc arc)
        {
            return new Arc(-arc.LongitudeDifference, -arc.LatitudeDifference);
        }

        public static Arc operator +(Arc arc)
        {
            return arc;
        }

        public override string ToString()
        {
            return LongitudeDifference.ToString() + " " + LatitudeDifference.ToString();
        }
    }

    // przeslanianie operatora == i !=
    public sealed class ProductSerialNumber
    {
        public static bool operator ==(ProductSerialNumber leftHandSide, ProductSerialNumber rightHandSide)
        {
            if (leftHandSide is null)
            {
                return rightHandSide is null;
            }
            return leftHandSide.Equals(rightHandSide);
        }

        public static bool operator !=(ProductSerialNumber leftHandSide, ProductSerialNumber rightHandSide)
        {
            return !(leftHandSide == rightHandSide);
        }
    }
}

// 426