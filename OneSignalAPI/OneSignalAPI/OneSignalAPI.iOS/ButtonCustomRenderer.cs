using CoreAnimation;
using CoreGraphics;
using OneSignalAPI;
using OneSignalAPI.iOS;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;



[assembly: ExportRenderer(typeof(CustomButtonText), typeof(ButtonCustomRenderer))]
namespace OneSignalAPI.iOS
{
    class ButtonCustomRenderer : ButtonRenderer
    {
        UIButton button;

        private CustomButtonText _label;
        public override void Draw(CoreGraphics.CGRect rect)
        {
            base.Draw(rect);

            var gradientView = new UIView(Control.Frame);
            Control.BackgroundColor = UIColor.Clear;

            var gradientLayer = new CAGradientLayer
            {
                Frame = gradientView.Layer.Bounds,
                Colors = new CGColor[] { _label.StartColor.ToCGColor(), _label.EndColor.ToCGColor() }
            };

            gradientView.Layer.AddSublayer(gradientLayer);
            gradientView.AddSubview(Control);

            var gradientLabel = new UIButton(Control.Frame);
            gradientLabel.AddSubview(gradientView);
            SetNativeControl(gradientLabel);

            if (_label.Orientation == StackOrientation.Vertical)
            {
                gradientLayer.StartPoint = new CGPoint(0.5, 0);
                gradientLayer.EndPoint = new CGPoint(0.5, 1.0);
            }
            else
            {
                gradientLayer.StartPoint = new CGPoint(0, 0.5);
                gradientLayer.EndPoint = new CGPoint(1.0, 0.5);
            }

            base.Draw(rect);

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null) return;
            if (Equals(_label, null))
                _label = e.NewElement as CustomButtonText;
        }
    }
}
