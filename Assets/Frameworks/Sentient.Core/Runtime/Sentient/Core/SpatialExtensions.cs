using System;
using UnityEngine;

namespace Sentient
{
    /// <summary>
    /// <see cref="UnityEngine"/> extensions and methods related to spatial alignment and interaction. 
    /// </summary>
    public static class SpatialExtensions
    {
        /// <summary>
        /// Calculates the twist applied from transform <paramref name="affectorTransform"/> onto 
        /// any objects that twists around axis <paramref name="axisWorld"/>.
        /// </summary>
        /// <param name="axisWorld">The twist axis in world coordinates.</param>
        /// <param name="affectorTransform">The <see cref="Transform"/> whos rotational motion is causing the twist.</param>
        /// <param name="affectorLastRotation">The rotation of <paramref name="affectorTransform"/> in the previous frame.</param>
        public static float CalculateTwistDelta( Vector3 axisWorld, Transform affectorTransform, Quaternion affectorLastRotation )
        {
            // Get any vector orthogonal to the axis
            Vector3 orthogonalVector = Vector3.Cross( axisWorld, axisWorld == Vector3.up ? Vector3.left : Vector3.up ).normalized;

            // Convert ortho vector to local
            var vectorLocal = affectorTransform.InverseTransformVector( orthogonalVector );

            var rotatedOrtho = Vector3.ProjectOnPlane( affectorLastRotation * vectorLocal, axisWorld );
            var currentOrtho = Vector3.ProjectOnPlane( affectorTransform.rotation * vectorLocal, axisWorld );

            return Vector3.SignedAngle( rotatedOrtho, currentOrtho, axisWorld );
        }

        /// <summary>
        /// Changes the position and rotation of the specified transform so <paramref name="childTransform"/> becomes located
        /// along the specified segment, and that the <paramref name="childTransform"/> local vector 
        /// <paramref name="childAlignDirection"/> has the same direction as the segment.
        /// </summary>
        /// <param name="affectedTransform">The affected transform.</param>
        /// <param name="childTransform">The child transform.</param>
        /// <param name="childAlignDirection">A vector in <paramref name="childTransform"/>'s coordinate system that will become parallel
        /// to the segment.</param>
        /// <param name="segmentStart">Start position of the segment.</param>
        /// <param name="segmentEnd">End position of the segment.</param>
        /// <remarks>
        /// This method is intended to position  
        /// </remarks>
        public static void AlignChildWithSegment( this Transform affectedTransform, Transform childTransform, Vector3 childAlignDirection,
            Vector3 segmentStart, Vector3 segmentEnd )
        {
            throw new NotImplementedException();
        }

        public static void AlignChildWithSegment( this Transform affectedTransform, Transform childTransform, Vector3 segment1, Vector3 segment2,
            Quaternion childRotation )
        {
            var childPos = Vector3Extensions.ClosestPointOnSegment( childTransform.position, segment1, segment2 );
            SetChildPositionAndRotation( affectedTransform, childTransform, childPos, childRotation );
        }

        /// <summary>
        /// Positions <paramref name="childTransform"/> onto the specified <see cref="ray"/> without changing its local coordinates.
        /// </summary>
        /// <param name="affectedTransform">The affected transform.</param>
        /// <param name="childTransform">The child transform.</param>
        /// <param name="ray">The ray.</param>
        public static void AlignChildWithRay( this Transform affectedTransform, Transform childTransform, Ray ray )
        {
            SetChildPositionAndRotation( affectedTransform, childTransform, ray.ClosestPoint( childTransform.position ), childTransform.rotation );
        }

        /// <summary>
        /// Positions <paramref name="childTransform"/> onto the specified <see cref="ray"/> without changing its local coordanates.
        /// The direction of <paramref name="childRayAlignDirection"/> in <paramref name="childTransform"/>'s coordinates will be aligned
        /// along the ray.
        /// </summary>
        /// <param name="affectedTransform">The affected transform.</param>
        /// <param name="childTransform">The child transform.</param>
        /// <param name="ray">The ray.</param>
        /// <param name="childRayAlignDirection">A direction vector in <paramref name="childTransform"/> that will become aligned 
        /// with <see cref="Ray.direction"/>.</param>
        /// <exception cref="NotImplementedException"></exception>
        public static void AlignChildWithRay( this Transform affectedTransform, Transform childTransform, Ray ray, Vector3 childRayAlignDirection )
        {
            var childPos = ray.ClosestPoint( childTransform.position );
            var newChildRotation = childTransform.rotation * RotationBetween( childTransform, childRayAlignDirection, ray.direction );

            SetChildPositionAndRotation( affectedTransform, childTransform, childPos, newChildRotation );
        }

        /// <summary>
        /// Positions <paramref name="childTransform"/> onto the specified <see cref="ray"/> without changing its local coordinates.
        /// Additionally the <see cref="Transform.rotation"/> of the child will become <paramref name="childTransform"/>.
        /// </summary>
        /// <param name="affectedTransform">The affected transform.</param>
        /// <param name="childTransform">The child transform.</param>
        /// <param name="ray">The ray.</param>
        /// <param name="childRotation">The child's final <see cref="Transform.rotation"/>.</param>
        public static void AlignChildWithRay( this Transform affectedTransform, Transform childTransform, Ray ray, Quaternion childRotation )
        {
            var childPos = ray.ClosestPoint( childTransform.position );

            SetChildPositionAndRotation( affectedTransform, childTransform, childPos, childRotation );
        }

        public static void AlignChildRotation( this Transform affectedTransform, Transform childTransform, Vector3 childVector, Vector3 targetVector )
        {
            throw new NotImplementedException();
        }

        public static void AlignDirection( this Transform affectedTransform, Vector3 localDirection, Vector3 targetDirection )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Changes the position and rotation of <paramref name="affectedTransform"/> so that <paramref name="childTransform"/>'s world rotation matches
        /// <paramref name="position"/> and <paramref name="worldRotation"/>. The local coordinates of the child is not changed.
        /// </summary>
        /// <param name="affectedTransform">The affected transform.</param>
        /// <param name="childTransform">The child transform whos world position will match the specified values.</param>
        /// <param name="position">The target world position for <paramref name="childTransform"/>.</param>
        /// <param name="worldRotation">The target rotation for <paramref name="childTransform"/>.</param>
        public static void SetChildPositionAndRotation( this Transform affectedTransform, Transform childTransform, Vector3 position,
            Quaternion worldRotation )
        {
            var relativeParentRot = RotationBetween( childTransform.rotation, affectedTransform.rotation );
            var parentLocalPos = childTransform.InverseTransformPoint( affectedTransform.position );

            affectedTransform.SetPositionAndRotation( position + worldRotation * parentLocalPos, worldRotation * relativeParentRot );
        }

        /// <summary>
        /// Changes the position and rotation of <paramref name="parentTransform"/> so that its child <paramref name="childTransform"/> 
        /// will have the same position and rotation as <paramref name="targetTransform"/> in world coordinates.
        /// </summary>
        /// <param name="parentTransform">The parent <see cref="Transform"/> whos position and rotation will be modified.</param>
        /// <param name="childTransform">The <see cref="Transform"/> that will become aligned with <paramref name="targetTransform"/>.</param>
        /// <param name="targetTransform">The <see cref="Transform"/> that <paramref name="childTransform"/> will become aligned
        /// with after the method returns.</param>
        public static void AlignChildTransformWith( this Transform parentTransform, Transform childTransform, Transform targetTransform )
        {
            ThrowIfNotParented( parentTransform, childTransform );

            var rotFromChildToParent = RotationBetween( childTransform.rotation, parentTransform.rotation );
            parentTransform.rotation = targetTransform.rotation * rotFromChildToParent;

            // Position of the parent in the child transform's coordinate system.
            var parentPosRelativeToChild = childTransform.InverseTransformPoint( parentTransform.position );

            parentTransform.position = targetTransform.TransformPoint( parentPosRelativeToChild );
        }

        /// <summary>
        /// Throws an exception if the <paramref name="childTransform"/> is not a child of <paramref name="parentTransform"/>.
        /// </summary>
        /// <param name="parentTransform">The parent transform.</param>
        /// <param name="childTransform">The child transform.</param>
        /// <exception cref="ArgumentException">childTransform</exception>
        private static void ThrowIfNotParented( Transform parentTransform, Transform childTransform )
        {
            if ( !childTransform.IsChildOf( parentTransform ) )
                throw new ArgumentException(
                    $"{nameof(childTransform)} {childTransform.name} must be a child of {nameof(parentTransform)} {parentTransform.name}" );
        }

        /// <summary>
        /// Returns the <see cref="Quaternion"/> rotation to align <paramref name="childDirection"/> to <paramref name="worldDirection."/>
        /// </summary>
        /// <param name="transform">The transform.</param>
        /// <param name="childDirection">The direction in <paramref name="transform"/>'s coordinates.</param>
        /// <param name="worldDirection">The world direction.</param>
        /// <returns></returns>
        public static Quaternion RotationBetween( Transform transform, Vector3 childDirection, Vector3 worldDirection )
        {
            childDirection = transform.TransformDirection( childDirection );

            var rotateAxis = Vector3.Cross( childDirection, worldDirection );
            return Quaternion.AngleAxis( Vector3.SignedAngle( childDirection, worldDirection, rotateAxis ), rotateAxis );
        }

        /// <summary>
        /// Pivots the specified <see cref="Transform"/> around <paramref name="pivotWorld"/> so that <paramref name="childTransform"/> will be positioned
        /// onto or towards the specified segment, depending on whether <paramref name="childTransform"/> can be reached.
        /// </summary>
        /// <param name="pivotedTransform">The <see cref="Transform"/> being pivoted.</param>
        /// <param name="pivotWorld">The pivoting position in world coordinates.</param>
        /// <param name="childTransform">The child transform.</param>
        /// <param name="segmentA">The segment a.</param>
        /// <param name="segmentB">The segment b.</param>
        /// <exception cref="NotImplementedException"></exception>
        public static void PivotChildOntoSegment( this Transform pivotedTransform, Vector3 pivotWorld, Transform childTransform, Vector3 segmentA,
            Vector3 segmentB )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the signed angle of <paramref name="target"/> around axis <paramref name="axis"/> where <paramref name="zeroValue"/>
        /// indicates a rotation of 0. <paramref name="previousRotation"/> is also included to allow for tracking rotations outside 
        /// of the -180 to 180 range.
        /// </summary>
        /// <param name="zeroValue">An orthogonal vector around <paramref name="axis" /> that indicates a 0 rotational value.</param>
        /// <param name="target">The vector whos rotation around axis will be returned.</param>
        /// <param name="axis">The axis of rotation.</param>
        /// <param name="previousRotation">The previous rotation value required to maintain angles outside of -180 to 180.</param>
        /// <remarks>
        /// This is intended to allow tracking of rotating wheels, bolts, nuts where rotations may exceed 180 degrees from the zero point.
        /// </remarks>
        public static float SignedAngle360( Vector3 zeroValue, Vector3 target, Vector3 axis, float previousRotation )
        {
            // This should already be on the plane.
            zeroValue = Vector3.ProjectOnPlane( zeroValue, axis );

            // Calculate the previous rotation vector
            Vector3 previousTarget = Quaternion.AngleAxis( previousRotation, axis ) * zeroValue;

            float targetDelta = Vector3.SignedAngle( previousTarget, target, axis );

            return previousRotation + targetDelta;
        }

        /// <summary>
        /// Returns the relative <see cref="Quaternion"/> rotation from <paramref name="a"/> to get to <paramref name="b"/>.
        /// </summary>
        /// <param name="a">The first rotation.</param>
        /// <param name="b">The second rotation.</param>
        /// <remarks>
        /// <paramref name="a"/> * <see cref="RotationBetween"/>(<paramref name="a"/>, <paramref name="b"/>) is equal to <paramref name="b"/>.
        /// </remarks>
        public static Quaternion RotationBetween( Quaternion a, Quaternion b )
        {
            return Quaternion.Inverse( a ) * b;
        }

        /// <summary>
        /// Transforms the rotation <paramref name="localRotation"/> from local space to world space.
        /// </summary>
        public static Quaternion TransformQuaternion( this Transform transform, Quaternion localRotation )
        {
            return transform.rotation * localRotation;
        }

        /// <summary>
        /// Projects vector onto the <paramref name="unitVector"/> and returns the scalar multiple of <paramref name="unitVector"/>
        /// that matches the projected <paramref name="vector"/>.
        /// </summary>
        /// <param name="vector">The vector.</param>
        /// <param name="unitVector">The unit vector.</param>
        /// <returns>
        /// A multiple of <paramref name="unitVector"/> that equals the projection of <paramref name="vector"/> onto <paramref name="unitVector"/>.
        /// </returns>
        public static float GetUnitVectorComponent( Vector3 vector, Vector3 unitVector )
        {
            float num = Vector3.Dot( unitVector, unitVector );

            if ( (double) num < (double) Mathf.Epsilon )
                return 0;

            return Vector3.Dot( vector, unitVector ) / num;
        }

        /// <summary>
        /// Transforms the rotation <paramref name="globalRotation"/> from world space to local space.
        /// </summary>
        public static Quaternion InverseTransformQuaternion( this Transform transform, Quaternion globalRotation )
        {
            return RotationBetween( transform.rotation, globalRotation );
        }
    }
}