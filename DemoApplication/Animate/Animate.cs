using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DemoApplication
{
    public static class Animate
    {
        //
        public static async Task BallAnimate(View view, int Top, int reduce, int time)
        {
            await view.RelRotateTo(360, 1000);
            do
            {
                await view.TranslateTo(view.TranslationX, view.TranslationY - Top, 500, Easing.CubicOut);
                await view.TranslateTo(view.TranslationX, view.TranslationY + Top, 300, Easing.CubicIn);

                Top = Top - reduce;
                time--;
            } while (time != 0);

            /*
            await view.TranslateTo(view.TranslationX, view.TranslationY - 50, 500, Easing.Linear);
            await view.TranslateTo(view.TranslationX, view.TranslationY + 50, 300, Easing.Linear);
            await view.TranslateTo(view.TranslationX, view.TranslationY - 20, 300, Easing.Linear);
            await view.TranslateTo(view.TranslationX, view.TranslationY + 20, 150, Easing.Linear);
            await view.TranslateTo(view.TranslationX, view.TranslationY - 10, 150, Easing.Linear);
            await view.TranslateTo(view.TranslationX, view.TranslationY + 10, 100, Easing.Linear);
            await view.FadeTo(-0, 1000);
            */
        }

        public static async Task FadeOut(this VisualElement element, uint duration = 400, Easing easing = null)
        {
            await element.FadeTo(0, duration, easing);
            element.IsVisible = false;
        }

        public static async Task FadeIn(this VisualElement element, uint duration = 400, Easing easing = null)
        {
            await element.FadeTo(1, duration, easing);
            element.IsVisible = true;
        }

    }
}
