using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(Entry), typeof(JobSearch.App.UWP.Utility.Control.CustomEntryRenderer))]
namespace JobSearch.App.UWP.Utility.Control
{
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer()
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.BorderThickness = new Windows.UI.Xaml.Thickness(0);
            }
        }
    }
}
