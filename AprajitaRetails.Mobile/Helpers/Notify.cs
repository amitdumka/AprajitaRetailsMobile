using CommunityToolkit.Maui.Alerts;

namespace AprajitaRetails.Mobile.Helpers
{
    public static class Notify
    {
        public static void NotifyLong(string msg)
        {
            Toast.Make(msg, CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
        }

        public static void NotifyShort(string msg)
        {
            Toast.Make(msg, CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
        }

        public static void NotifyVLong(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                Toast.Make(msg, CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                ASpeak.Speak(msg);
            }
        }

        public static void NotifyVShort(string msg)
        {
            Toast.Make(msg, CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            ASpeak.Speak(msg);
        }
    }

    public class ASpeak
    {
        public static async void Speak(string text) => await TextToSpeech.Default.SpeakAsync(text);

        private CancellationTokenSource cts;

        public async Task SpeakNowDefaultSettingsAsync()
        {
            cts = new CancellationTokenSource();
            await TextToSpeech.Default.SpeakAsync("Hello World", cancelToken: cts.Token);

            // This method will block until utterance finishes.
        }

        // Cancel speech if a cancellation token exists & hasn't been already requested.
        public void CancelSpeech()
        {
            if (cts?.IsCancellationRequested ?? true)
                return;

            cts.Cancel();
        }

        private bool isBusy = false;

        public void SpeakMultiple(List<string> texts)
        {
            isBusy = true;
            Task[] tList = new Task[texts.Count];

            int i = 0;
            foreach (var text in texts)
                tList[i++] = (TextToSpeech.Default.SpeakAsync(text));

            Task.WhenAll(tList).ContinueWith((t) => { isBusy = false; },
                TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}