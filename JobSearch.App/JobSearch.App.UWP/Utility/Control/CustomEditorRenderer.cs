using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(Editor), typeof(JobSearch.App.UWP.Utility.Control.CustomEditorRenderer))]
namespace JobSearch.App.UWP.Utility.Control
{
    public class CustomEditorRenderer : EditorRenderer
    {
        public CustomEditorRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderThickness = new Windows.UI.Xaml.Thickness(0);
            }
        }
    }
}
