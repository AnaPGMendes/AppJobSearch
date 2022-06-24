using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(Entry), typeof(JobSearch.App.Droid.Utility.Controls.CustomEntryRenderer))] 
// ExportRenderer(typeof( *quem eu quero influenciar* ), typeof( *O responsável por renderizar essa aparência* )
namespace JobSearch.App.Droid.Utility.Controls
{
    //criando um entry costumizado que herda do Entry
    public class CustomEntryRenderer : EntryRenderer
    {
        public CustomEntryRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            // tirando a barra de escrita do Entry
            if (Control != null)
            {
                Control.Background = null;
                Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                // por o "Android.Graphics" para não dar conflito entre o Color do Android e o Color do Xamarin
            }
        }
    }
}