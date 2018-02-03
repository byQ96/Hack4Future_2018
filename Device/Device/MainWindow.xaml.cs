using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Management;

namespace Device
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private DispatcherTimer timer2;
        private Params param;
        private Params param2;
        public MainWindow()
        {
            InitializeComponent();
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(5.0);
            timer.Tick += Timer_Tick;
            timer.Start();
            param = new Params();
            param.device_id = 1;
            param.group_id = 1;


            timer2 = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromSeconds(7.0);
            timer2.Tick += Timer_Tick2;
            timer2.Start();
            param2 = new Params();
            param2.device_id = 1;
            param2.group_id = 2;
            //param.value = 1;
        }

        private async void Timer_Tick(object sender, object e)
        {
            ManagementObjectSearcher info = new ManagementObjectSearcher("SELECT CurrentClockSpeed FROM Win32_Processor");
            ManagementObjectCollection baza = info.Get();

            foreach (ManagementObject dane in baza)
            {
                param.value = dane["CurrentClockSpeed"].ToString();
                //Console.Write(dane["CurrentVoltage"].ToString() + Environment.NewLine);
            }
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(@"http://25.69.90.179:8101/");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));

                string endpoint = @"api/addParams";

                try
                {
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(param), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(endpoint, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        //do something with json response here
                        // textBlock1.Text = jsonResponse;
                        //Task.Delay(3000).Wait();
                        // this.Frame.Navigate(typeof(PrintPage), parameters);
                        return;
                    }
                    else
                    {
                        // textBlock1.Text = response.ToString();
                    }

                }
                catch (Exception ex)
                {
                    //textBlock1.Text = ex.Message;
                    //Could not connect to server
                    //Use more specific exception handling, this is just an example
                }
            }
        }

        private async void Timer_Tick2(object sender, object e)
        {
            Random rnd = new Random();
            param2.value = rnd.Next(50,60).ToString();

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(@"http://25.69.90.179:8101/");
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("utf-8"));

                string endpoint = @"api/addParams";

                try
                {
                    HttpContent content = new StringContent(JsonConvert.SerializeObject(param2), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await httpClient.PostAsync(endpoint, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();
                        //do something with json response here
                        // textBlock1.Text = jsonResponse;
                        //Task.Delay(3000).Wait();
                        // this.Frame.Navigate(typeof(PrintPage), parameters);
                        return;
                    }
                    else
                    {
                        // textBlock1.Text = response.ToString();
                    }

                }
                catch (Exception ex)
                {
                    //textBlock1.Text = ex.Message;
                    //Could not connect to server
                    //Use more specific exception handling, this is just an example
                }
            }
        }
    }
}
