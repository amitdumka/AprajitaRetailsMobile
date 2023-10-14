using AprajitaRetails.Mobile.AppSettings;
using AprajitaRetails.Shared.Models.Auth;
using AprajitaRetails.Shared.ViewModels;
using System.Text;

namespace AprajitaRetails.Mobile.RemoteServices
{
    public class RestService
    {
        private static HttpClient _client;
        private static JsonSerializerOptions _serializerOptions;

        private static string authorizationKey;

        public RestService()
        {
            _client = GetAuthClient();
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        private static HttpClient GetAuthClient()
        {
            if (_client != null)
                return _client;

            var handler2 = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
       HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
            };
            _client = new HttpClient(handler2);
            // new HttpClient();
            //_client = new HttpClient();
            // if (string.IsNullOrEmpty(authorizationKey))
            // {
            //     authorizationKey = ClientSetting.GetSecureAsync("AuthToken").Result;
            // }

            // _client.DefaultRequestHeaders.Add("Authorization", authorizationKey);
            _client.DefaultRequestHeaders.Add("Accept", "application/json");

            return _client;
        }

        private static HttpClient GetClient()
        {
            //HttpsClientHandlerService handler = new HttpsClientHandlerService();
            HttpsClientHandlerService handler = new HttpsClientHandlerService();
            var handler2 = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
            };
            HttpClient client = new HttpClient(handler2);// new HttpClient();
            //HttpClient client = new HttpClient(handler.GetPlatformMessageHandler());// new HttpClient();
            client.BaseAddress = new Uri(Constants.RestUrl);
            _serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public static async Task<LoggedUser> DoLoginAsync(string UserName, string Password)
        {
            var client = GetClient();
            Uri uri = new Uri($"{Constants.RestUrl}Auths");
            try
            {
                string json = JsonSerializer.Serialize<LoginVM>(new LoginVM { UserName = UserName, Password = Password, RememberMe = true, StoreId = "ARD" }, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, uri);
                message.Content = content;

                HttpResponseMessage response = await client.SendAsync(message);

                //HttpResponseMessage response = await client.PostAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    string rescontent = await response.Content.ReadAsStringAsync();
                    ClientSetting.SetPref("LoggedUser", rescontent);

                    LoggedUser user = JsonSerializer.Deserialize<LoggedUser>(rescontent);//, _serializerOptions);
                    ClientSetting.SetPostLogin(user);
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                //TODO: notif user
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                return null;
            }
        }

        //public async Task SaveTodoItemAsync(TodoItem item, bool isNewItem = false)
        //{
        //    response = await _client.PutAsync(uri, content);
        //}

        #region GetRegion

        public async Task<List<T>> GetAllAsync<T>(string apiUrl)
        {
            Uri uri = new Uri($"{Constants.RestUrl}{apiUrl}");
            Notify.NotifyLong(uri.ToString());
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<List<T>>(content);
                    return data;
                }
                else
                {
                    Notify.NotifyLong($"\tERROR {response.StatusCode} # {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Notify.NotifyLong($"\tERROR {ex.Message}");
                return null;
            }
        }

        public async Task<T> GetByIdAsync<T>(string apiUrl, string Id)
        {
            Uri uri = new Uri($"{Constants.RestUrl}{apiUrl}/{Id}");
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<T>(content, _serializerOptions);
                    return data;
                }
                else
                {
                    Notify.NotifyLong($"\tERROR {response.StatusCode} # {response.ReasonPhrase}");
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Notify.NotifyLong($"\tERROR {ex.Message}");
                return default(T);
            }
        }

        public async Task<T> GetByIdAsync<T>(string apiUrl, int Id)
        {
            Uri uri = new Uri($"{Constants.RestUrl}{apiUrl}/{Id}");
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<T>(content, _serializerOptions);
                    return data;
                }
                else
                {
                    Notify.NotifyLong($"\tERROR {response.StatusCode} # {response.ReasonPhrase}");
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Notify.NotifyLong($"\tERROR {ex.Message}");
                return default(T);
            }
        }

        public async Task<T> GetByIdAsync<T>(string apiUrl, Guid Id)
        {
            Uri uri = new Uri($"{Constants.RestUrl}{apiUrl}/{Id}");
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<T>(content, _serializerOptions);
                    return data;
                }
                else
                {
                    Notify.NotifyLong($"\tERROR {response.StatusCode} # {response.ReasonPhrase}");
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Notify.NotifyLong($"\tERROR {ex.Message}");
                return default(T);
            }
        }

        public async Task<T> GetByIdAsync<T>(string apiUrl, string idName, string Id)
        {
            Uri uri = new Uri($"{Constants.RestUrl}{apiUrl}?{idName}={Id}");
            try
            {
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<T>(content, _serializerOptions);
                    return data;
                }
                else
                {
                    Notify.NotifyLong($"\tERROR {response.StatusCode} # {response.ReasonPhrase}");
                    return default(T);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Notify.NotifyLong($"\tERROR {ex.Message}");
                return default(T);
            }
        }

        #endregion GetRegion

        #region DeleteRegion

        public async Task<bool> DeleteAsync(string apiUri, Guid id)
        {
            Uri uri = new Uri($"{Constants.RestUrl}{apiUri}/{id}");

            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully deleted.");
                    Notify.NotifyVShort("Deleted successfully!");
                    return true;
                }
                else
                {
                    Debug.WriteLine(@"\tERROR {0}", response.StatusCode);
                    Notify.NotifyVShort($"\tERROR {response.ReasonPhrase}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Notify.NotifyVShort($"\tERROR {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string apiUri, string id)
        {
            Uri uri = new Uri($"{Constants.RestUrl}{apiUri}/{id}");

            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully deleted.");
                    Notify.NotifyVShort("Deleted successfully!");
                    return true;
                }
                else
                {
                    Debug.WriteLine(@"\tERROR {0}", response.StatusCode);
                    Notify.NotifyVShort($"\tERROR {response.ReasonPhrase}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Notify.NotifyVShort($"\tERROR {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string apiUri, int id)
        {
            Uri uri = new Uri($"{Constants.RestUrl}{apiUri}/{id}");

            try
            {
                HttpResponseMessage response = await _client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine(@"\tTodoItem successfully deleted.");
                    Notify.NotifyVShort("Deleted successfully!");
                    return true;
                }
                else
                {
                    Debug.WriteLine(@"\tERROR {0}", response.StatusCode);
                    Notify.NotifyVShort($"\tERROR {response.ReasonPhrase}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Notify.NotifyVShort($"\tERROR {ex.Message}");
                return false;
            }
        }

        #endregion DeleteRegion

        #region SaveRegion

        public async Task<object> SaveAsync<T>(string apiurl, T item, bool isNewItem = false)
        {
            Uri uri = new Uri(string.Concat(Constants.RestUrl, apiurl));

            try
            {
                string json = JsonSerializer.Serialize<T>(item, _serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;

                if (isNewItem)
                    response = await _client.PostAsync(uri, content);
                else
                    response = await _client.PutAsync(uri, content);

                if (response.IsSuccessStatusCode)
                {
                    if (isNewItem)
                    {
                        Notify.NotifyVShort("Save successfully");
                    }
                    else Notify.NotifyVShort("Updated successfully");
                    return item;
                }
                else
                {
                    if (isNewItem)
                    {
                        Notify.NotifyVShort($"Failed Save, {response.ReasonPhrase}");
                    }
                    else Notify.NotifyVShort($"Failed update, {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Notify.NotifyVShort($"Error, {ex.Message}");
                return null;
            }
        }

        #endregion SaveRegion

        public static async Task<List<SelectOption>> GetStoreListAsync()
        {
            var client = GetAuthClient();
            Uri uri = new Uri($"{Constants.RestUrl}helper/stores");
            // Notify.NotifyLong(uri.ToString());
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                //   Notify.NotifyShort(await response.Content.ReadAsStringAsync());
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<List<SelectOption>>(content);
                    //foreach (var item in data)
                    //{
                    //    item.Value = item.Value.Trim().ToString();
                    //    item.ID = item.ID.Trim().ToString();
                    //}
                    return data;
                }
                else
                {
                    Notify.NotifyLong($"\tERROR {response.StatusCode} # {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Notify.NotifyLong($"\tERROR {ex.Message}");
                return null;
            }
        }

        public static async Task<List<SelectOption>> GetEmployeeListAsync(string storeid)
        {
            var client = GetClient();
            Uri uri = new Uri($"{Constants.RestUrl}helper/Employees?StoreId={storeid}");
            // Notify.NotifyLong(uri.ToString());
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<List<SelectOption>>(content);
                    //foreach (var item in data)
                    //{
                    //    item.Value = item.Value.Trim().ToString();
                    //    item.ID = item.ID.Trim().ToString();
                    //}
                    return data;
                }
                else
                {
                    Notify.NotifyLong($"\tERROR {response.StatusCode} # {response.ReasonPhrase}");
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
                Notify.NotifyLong($"\tERROR {ex.Message}");
                return null;
            }
        }
    }

    public class RestAPI
    {
    }
}