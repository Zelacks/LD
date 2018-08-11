using UnityEngine;


namespace Sentient
{

    public static class Vector3Extensions
    {

        /// <summary>
        /// Returns the closest <see cref="Vector3"/> on the segment towards <paramref name="point"/>.
        /// </summary>
        /// <param name="point">The point .</param>
        /// <param name="segmentP1">The segment start point.</param>
        /// <param name="segmentP2">The segment end point.</param>
        /// <returns></returns>
        public static Vector3 ClosestPointOnSegment( Vector3 point, Vector3 segmentP1, Vector3 segmentP2 )
        {
            Vector3 segVector = segmentP2 - segmentP1;

            float segDot = Vector3.Dot( segVector, segVector );

            float seg2PointDot = Vector3.Dot( point - segmentP1, segVector );

            if ( seg2PointDot <= Mathf.Epsilon )
                return segmentP1;
            else if ( seg2PointDot >= segDot - Mathf.Epsilon )
                return segmentP2;
            else
            {
                // Use double for accuracy
                double percentAlongSeg = seg2PointDot / segDot;

                return segmentP1 + new Vector3( (float) ( segVector.x * percentAlongSeg ), (float) ( segVector.y * percentAlongSeg ),
                                                (float) ( segVector.z * percentAlongSeg ) );
            }
        }

        /// <summary>
        /// Closests the point on ray.
        /// </summary>
        /// <param name="point">The point.</param>
        /// <param name="ray">The ray.</param>
        /// <returns></returns>
        public static Vector3 ClosestPointOnRay( Vector3 point, Ray ray )
        {
            float distAlongRay = Vector3.Dot( point - ray.origin, ray.direction );

            if ( distAlongRay <= 0 )
                return ray.origin;

            return ray.origin + distAlongRay * ray.direction;
        }

        /// <summary>
        /// Returns the closest <see cref="Vector3"/> on the ray to <paramref name="point"/>.
        /// </summary>
        public static Vector3 ClosestPoint( this Ray ray, Vector3 point )
        {
            return ClosestPointOnRay( point, ray );
        }

        //        public static float ClosestPointOnSegmnentToSegment( Vector3 seg1A, Vector3 seg1B, Vector3 seg2A, Vector3 seg2B, out Vector3 closestSeg1,
        //            out Vector3 closestSeg2 )
        //        {
        //            Vector3 seg1Vec = seg1B - seg1A;
        //            Vector3 seg2Vec = seg2B - seg2A;
        //
        //            Vector3 b1ToA1 = seg1A - seg2A;
        //
        //            float a = Vector3.Dot( seg1Vec, seg1Vec ); // always >= 0
        //            float b = Vector3.Dot( seg1Vec, seg2Vec );
        //            float c = Vector3.Dot( seg2Vec, seg2Vec ); // always >= 0
        //            float d = Vector3.Dot( seg1Vec, b1ToA1 );
        //            float e = Vector3.Dot( seg2Vec, b1ToA1 );
        //
        //            float D = a * c - b * b; // always >= 0
        //            float sc, sN, sD = D; // sc = sN / sD, default sD = D >= 0
        //            float tc, tN, tD = D; // tc = tN / tD, default tD = D >= 0
        //        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="local"></param>
        /// <param name="global"></param>
        /// <returns></returns>
        public static Vector3 Offset(this Transform t, Vector3 local, Vector3 global)
        {
            return t.position + global + t.right * local.x + t.up * local.y + t.forward * local.z;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="local"></param>
        /// <param name="global"></param>
        /// <returns></returns>
        public static Vector3 Offset(this Component t, Vector3 local, Vector3 global)
        {
            return t.transform.position + global + t.transform.right * local.x + t.transform.up * local.y + t.transform.forward * local.z;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Vector3 SetX(Vector3 v, float val)
        {
            var result = new Vector3(val, v.y, v.z);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Vector3 SetY(Vector3 v, float val)
        {
            var result = new Vector3(v.x, val, v.z);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static Vector3 SetZ(Vector3 v, float val)
        {
            var result = new Vector3(v.x, v.y, val);
            return result;
        }

        /// <summary>
        /// Returns an arbitrary Vector3 perpendicular to the input Vector,
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Vector3 ArbitraryPerpVector(this Vector3 input)
        {
            var inNorm = input.normalized;
            return Mathf.Abs(Vector3.Dot(inNorm, Vector3.up)) < 1.0f ? Vector3.Cross(inNorm, Vector3.up).normalized : Vector3.Cross(inNorm, Vector3.right).normalized;
        }




        public static Vector3 MultiplyComponentWise(this Vector3 a, Vector3 b)
        {
            return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
        }

        public static Vector3 InvertComponentWise(this Vector3 a)
        {
            return new Vector3(1 / a.x, 1 / a.y, 1 / a.z);
        }

        public static Vector3 DivideComponentWise(this Vector3 a, Vector3 b)
        {
            return a.MultiplyComponentWise(b.InvertComponentWise());
        }

        public static Vector3 ClampComponentWise(this Vector3 a, float min, float max)
        {
            return new Vector3(Mathf.Clamp(a.x, min, max), Mathf.Clamp(a.y, min, max), Mathf.Clamp(a.z, min, max));
        }

    }

}