using System;

using UnityEngine;
using UnityEngine.UI;


namespace Sentient.UserInterface
{

    /// <summary>
    /// Sets the size of one axis based on the size of the other.
    /// </summary>
    [ExecuteInEditMode]
    public class DependentSize : MonoBehaviour
    {

        /// <summary>
        /// The axis that is dependent on the other axis.
        /// </summary>
        public RectTransform.Axis DependentAxis = RectTransform.Axis.Horizontal;

        /// <summary>
        /// The relative size (1.0 = same as independent axis).
        /// </summary>
        public Single RelativeSize = 1.0f;

        /// <summary>
        /// The absolute pixel size offset.
        /// </summary>
        public Single AbsoluteOffset = 0.0f;

        /// <summary>
        /// An optional <see cref="LayoutElement"/> if you want to use this instead of manual layout.
        /// </summary>
        public LayoutElement Layout;




        /// <summary>
        /// Gets the <see cref="RectTransform"/>.
        /// </summary>
        protected RectTransform Rect { get { if ( !_Rect ) _Rect = GetComponent<RectTransform>( ); return _Rect; } }
        private RectTransform _Rect;

        /// <summary>
        /// Gets or sets the cached size of the independent axis.
        /// </summary>
        private Single _CachedSize { get; set; }




        /// <summary>
        /// Gets or sets the size of the dependent axis.
        /// </summary>
        protected Single DependentAxisSize
        {
            get { return DependentAxis == RectTransform.Axis.Horizontal ? Rect.rect.width : Rect.rect.height; }
            set
            {
                if ( Layout )
                {
                    if ( DependentAxis == RectTransform.Axis.Horizontal )
                        Layout.preferredWidth = value;
                    else
                        Layout.preferredHeight = value;
                }
                else
                    Rect.SetSizeWithCurrentAnchors( DependentAxis, value );
            }
        }

        /// <summary>
        /// Gets the size of the independent axis.
        /// </summary>
        protected Single IndependentAxisSize { get { return DependentAxis == RectTransform.Axis.Horizontal ? Rect.rect.height : Rect.rect.width; } }




        /// <summary>
        /// Breakfast, yay~!
        /// </summary>
        protected virtual void OnEnable( )
        {

#if UNITY_EDITOR
            if ( !Layout && !Application.isPlaying )
                Layout = GetComponent<LayoutElement>( );
#endif

            UpdateSize( );
        }

        /// <summary>
        /// Breakfast, yay~!
        /// </summary>
        protected virtual void Update( )
        {

            //#if UNITY_EDITOR
            //            if ( !Application.isPlaying )
            //                UpdateSize( );
            //#endif
            if ( !_CachedSize.Approx( IndependentAxisSize ) )
                UpdateSize( );
        }

        /// <summary>
        /// Updates the size of the object.
        /// </summary>
        [ContextMenu( "Update Size" )]
        public void UpdateSize( )
        {
            _CachedSize = IndependentAxisSize;
            var targetSize = _CachedSize * RelativeSize + AbsoluteOffset;

            if ( targetSize > 0 )
                DependentAxisSize = targetSize;
        }

    }
}