namespace Rhymba.Tests
{
    using Rhymba.Models.Common;
    using Rhymba.Models.Codes;
    using Rhymba.Models.Downloads;
    using Rhymba.Models.Playlist;
    using Rhymba.Models.Purchases;
    using Rhymba.Models.Search;
    using Rhymba.Models.Streaming;
    using RichardSzalay.MockHttp;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Web;
    using Rhymba.Services.Content;
    using Newtonsoft.Json.Linq;
    using System.Net.Http.Headers;

    internal class MockData
    {
        private readonly MockHttpMessageHandler mockHttpMessageHandler;

        internal MockData() 
        {
            this.mockHttpMessageHandler = new MockHttpMessageHandler();
        }

        internal HttpClient GetHttpClient()
        {
            return this.mockHttpMessageHandler.ToHttpClient();
        }

        internal void SetupCodes()
        {
            // create code
            this.mockHttpMessageHandler.When("https://dispatch.mcnemanager.com/current/content.odata/CreateCode/*").Respond(req => new HttpResponseMessage()
            {
                Content = JsonContent.Create<CreateCreditCodeResponse>(new CreateCreditCodeResponse()
                {
                    code = Guid.NewGuid(),
                    name = "rhymba_netcore_sdk_tests"
                }),
                StatusCode = System.Net.HttpStatusCode.OK
            });

            // edit code
            this.mockHttpMessageHandler.When("https://dispatch.mcnemanager.com/current/content.odata/EditCode/*").Respond(req => new HttpResponseMessage()
            {
                Content = new StringContent(string.Empty),
                StatusCode = System.Net.HttpStatusCode.OK
            });

            // get code
            this.mockHttpMessageHandler.When("https://dispatch.mcnemanager.com/current/content.odata/GetCode/*").Respond((req) =>
            {
                var codeStr = HttpUtility.ParseQueryString(req.RequestUri?.Query ?? string.Empty).Get("code");
                // "guid'00000000-0000-0000-0000-000000000000'"
                if (!string.IsNullOrWhiteSpace(codeStr))
                {
                    codeStr = codeStr.Substring(4).Trim('\'');
                    if (Guid.TryParse(codeStr, out var code))
                    {
                        return new HttpResponseMessage()
                        {
                            Content = JsonContent.Create<GetCodeResponse>(new GetCodeResponse()
                            {
                                amount = 100.00m,
                                code = code
                            }),
                            StatusCode = System.Net.HttpStatusCode.OK
                        };
                    }
                }

                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            });
        }

        internal void SetupDownloads()
        {
            // create download session
            this.mockHttpMessageHandler.When("https://dispatch.mcnemanager.com/current/content.odata/CreateDownloadSession/*").Respond(req => new HttpResponseMessage()
            {
                Content = JsonContent.Create<CreateDownloadSessionResponse>(new CreateDownloadSessionResponse()
                {
                    token = Guid.NewGuid().ToString()
                }),
                StatusCode = System.Net.HttpStatusCode.OK
            });

            // get download session
            this.mockHttpMessageHandler.When("https://dispatch.mcnemanager.com/current/content.odata/GetDownloadSessionInformation/*").Respond((req) =>
            {
                var token = HttpUtility.ParseQueryString(req.RequestUri?.Query ?? string.Empty).Get("token");
                if (!string.IsNullOrWhiteSpace(token))
                {
                    token = token.Trim('\'');
                    return new HttpResponseMessage()
                    {
                        Content = JsonContent.Create<GetDownloadSessionInformationResponse>(new GetDownloadSessionInformationResponse()
                        {
                            token = token
                        }),
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }

                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            });

            // update download session
            this.mockHttpMessageHandler.When("https://dispatch.mcnemanager.com/current/content.odata/UpdateDownloadSession/*").Respond((req) =>
            {
                var token = HttpUtility.ParseQueryString(req.RequestUri?.Query ?? string.Empty).Get("token");
                if (!string.IsNullOrWhiteSpace(token))
                {
                    token = token.Trim('\'');
                    return new HttpResponseMessage()
                    {
                        Content = JsonContent.Create<UpdateDownloadSessionResponse>(new UpdateDownloadSessionResponse()
                        {
                            token = token
                        }),
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }

                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            });

            // mark session complete
            this.mockHttpMessageHandler.When("https://dispatch.mcnemanager.com/current/content.odata/MarkSessionComplete/*").Respond(req => new HttpResponseMessage()
            {
                Content = new StringContent(string.Empty),
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }

        internal void SetupStreaming()
        {
            // get stream
            this.mockHttpMessageHandler.When("https://dispatch.mcnemanager.com/current/content.odata/GetStream/*").Respond((req) =>
            {
                var mediaIdStr = HttpUtility.ParseQueryString(req.RequestUri?.Query ?? string.Empty).Get("mediaId");
                if (!string.IsNullOrWhiteSpace(mediaIdStr) && int.TryParse(mediaIdStr, out var mediaId))
                {
                    return new HttpResponseMessage()
                    {
                        Content = JsonContent.Create<GetStreamResponse>(new GetStreamResponse()
                        {
                            id = mediaId,
                            url_segment_one = "https://hls.mcnemanager.com/current/"
                        }),
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }

                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            });

            // get streams
            this.mockHttpMessageHandler.When("https://dispatch.mcnemanager.com/current/content.odata/GetStreams/*").Respond((req) =>
            {
                var mediaIdStr = HttpUtility.ParseQueryString(req.RequestUri?.Query ?? string.Empty).Get("mediaIds");
                if (!string.IsNullOrWhiteSpace(mediaIdStr))
                {
                    var mediaIds = mediaIdStr.Trim('\'').Split(",").Select(x => int.Parse(x)).ToArray();

                    var getStreamsResponse = new GetStreamResponse[mediaIds.Length];
                    for (var x = 0; x < mediaIds.Length; x++)
                    {
                        getStreamsResponse[x] = new GetStreamResponse()
                        {
                            id = mediaIds[x],
                            url_segment_one = "https://hls.mcnemanager.com/current/"
                        };
                    }

                    return new HttpResponseMessage()
                    {
                        Content = JsonContent.Create<GetStreamResponse[]>(getStreamsResponse),
                        StatusCode = System.Net.HttpStatusCode.OK
                    };
                }

                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            });

            // get playlist stream
            this.mockHttpMessageHandler.When("https://dispatch.mcnemanager.com/current/content.odata/GetPlaylistStream/*").Respond(req => new HttpResponseMessage()
            {
                Content = JsonContent.Create<GetStreamResponse[]>(new GetStreamResponse[] 
                {  
                    new GetStreamResponse()
                    {
                        url_segment_one = "https://hls.mcnemanager.com/current"
                    }
                }),
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }

        internal void SetupImages()
        {
            // upload image
            this.mockHttpMessageHandler.When("https://ib3-lb.mcnemanager.com/new.imgup*").Respond(req => new HttpResponseMessage()
            {
                Content = new StringContent(Guid.NewGuid().ToString()),
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }

        internal void SetupPurchases()
        {
            // get invalid items
            this.mockHttpMessageHandler.When("https://purchases.mcnemanager.com/Purchases.svc/GetInvalidItems/*").Respond(req => new HttpResponseMessage()
            {
                Content = JsonContent.Create<InvalidItem[]>(Array.Empty<InvalidItem>()),
                StatusCode = System.Net.HttpStatusCode.OK
            });

            // report purchase
            this.mockHttpMessageHandler.When("https://purchases.mcnemanager.com/Purchases.svc/ReportPurchase/*").Respond(req => new HttpResponseMessage()
            {
                Content = JsonContent.Create<ReportPurchaseResponse>(new ReportPurchaseResponse()
                {
                    token = Guid.NewGuid().ToString(),
                }),
                StatusCode = System.Net.HttpStatusCode.OK
            });

            // finalize purchase
            this.mockHttpMessageHandler.When("https://purchases.mcnemanager.com/Purchases.svc/FinalizePurchase/*").Respond(req => new HttpResponseMessage()
            {
                Content = JsonContent.Create<FinalizePurchaseResponse>(new FinalizePurchaseResponse()
                {
                    purchaseid = Guid.NewGuid().ToString()
                }),
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }

        internal void SetupSearch()
        {
            // album search
            this.mockHttpMessageHandler.When("https://search.mcnemanager.com/v4/odata/Albums*").Respond(res => new HttpResponseMessage()
            {
                Content = JsonContent.Create<ODataValueWrapper<Album[]>>(new ODataValueWrapper<Album[]>()
                {
                    value = new Album[] 
                    { 
                        new Album()
                        {
                            name = "1989 (Taylor's Version)"
                        } 
                    }
                }),
                StatusCode = System.Net.HttpStatusCode.OK
            });

            // artist search
            this.mockHttpMessageHandler.When("https://search.mcnemanager.com/v4/odata/Artists*").Respond(res => new HttpResponseMessage()
            {
                Content = JsonContent.Create<ODataValueWrapper<Artist[]>>(new ODataValueWrapper<Artist[]>()
                {
                    value = new Artist[]
                    {
                        new Artist()
                        {
                            name = "Taylor Swift"
                        }
                    }
                }),
                StatusCode = System.Net.HttpStatusCode.OK
            });

            // media search
            this.mockHttpMessageHandler.When("https://search.mcnemanager.com/v4/odata/Medias*").Respond(res => new HttpResponseMessage()
            {
                Content = JsonContent.Create<ODataValueWrapper<Media[]>>(new ODataValueWrapper<Media[]>()
                {
                    value = new Media[]
                    {
                        new Media()
                        {
                            title = "Shake it Off"
                        }
                    }
                }),
                StatusCode = System.Net.HttpStatusCode.OK
            });
        }

        internal void SetupPlaylist()
        {
            var playlist = new PlaylistResponse()
            {
                Items = new List<PlaylistItem>(),
                PlaylistId = 1
            };

            // register user
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/account/register*").Respond(res => new HttpResponseMessage()
            {
                Content = JsonContent.Create<PlaylistResponseBase<int>>(new PlaylistResponseBase<int>()
                {
                    data = 1,
                    success = true
                }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                StatusCode = System.Net.HttpStatusCode.OK
            });

            // login
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/account/login*").Respond(res => new HttpResponseMessage()
            {
                Content = JsonContent.Create<PlaylistResponseBase<AccountLoginResponse>>(new PlaylistResponseBase<AccountLoginResponse>()
                {
                    data = new AccountLoginResponse()
                    {
                        Token = "token",
                    },
                    success = true
                }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                StatusCode = System.Net.HttpStatusCode.OK
            });

            // create a playlist
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/playlists").With(req => req.Method == HttpMethod.Post).Respond(res => new HttpResponseMessage()
            {
                Content = JsonContent.Create<PlaylistResponseBase<CreatePlaylistResponse>>(new PlaylistResponseBase<CreatePlaylistResponse>()
                {
                    data = new CreatePlaylistResponse()
                    {
                        PlaylistId = playlist.PlaylistId,
                    }
                }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                StatusCode = System.Net.HttpStatusCode.OK
            });

            // add album to playlist
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/playlists/*/albums").With(req => req.Method == HttpMethod.Post).Respond((res) =>
            {
                playlist.Items.Add(new PlaylistItem()
                {
                    AlbumId = 1,
                });

                return new HttpResponseMessage()
                {
                    Content = JsonContent.Create<PlaylistResponseBase<int>>(new PlaylistResponseBase<int>()
                    {
                        data = 1
                    }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            });

            // add artist to playlist
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/playlists/*/artists").With(req => req.Method == HttpMethod.Post).Respond((res) =>
            {
                playlist.Items.Add(new PlaylistItem()
                {
                    ArtistId = 1,
                });

                return new HttpResponseMessage()
                {
                    Content = JsonContent.Create<PlaylistResponseBase<int>>(new PlaylistResponseBase<int>()
                    {
                        data = 1
                    }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            });

            // add media to playlist
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/playlists/*/media").With(req => req.Method == HttpMethod.Post).Respond((res) =>
            {
                playlist.Items.Add(new PlaylistItem()
                {
                    MediaId = 1,
                });

                return new HttpResponseMessage()
                {
                    Content = JsonContent.Create<PlaylistResponseBase<int>>(new PlaylistResponseBase<int>()
                    {
                        data = 1
                    }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            });

            // add playlist to playlist
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/playlists/*/playlists").With(req => req.Method == HttpMethod.Post).Respond((res) =>
            {
                playlist.Items.Add(new PlaylistItem()
                {
                    id = 1,
                });

                return new HttpResponseMessage()
                {
                    Content = JsonContent.Create<PlaylistResponseBase<int>>(new PlaylistResponseBase<int>()
                    {
                        data = 1
                    }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            });

            // add user to playlist
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/playlists/*/users").With(req => req.Method == HttpMethod.Post).Respond(res => new HttpResponseMessage()
            {
                Content = JsonContent.Create<PlaylistResponseBase<PlaylistResponse>>(new PlaylistResponseBase<PlaylistResponse>()
                {
                    data = new PlaylistResponse()
                    {
                        PlaylistId = playlist.PlaylistId,
                    }
                }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                StatusCode = System.Net.HttpStatusCode.OK
            });

            // get playlist
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/playlists/1").With(req => req.Method == HttpMethod.Get).Respond(res => new HttpResponseMessage()
            {
                Content = JsonContent.Create<PlaylistResponseBase<PlaylistResponse>>(new PlaylistResponseBase<PlaylistResponse>()
                {
                    data = playlist
                }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                StatusCode = System.Net.HttpStatusCode.OK
            });

            // get playlist albums
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/playlists/*/albums").With(req => req.Method == HttpMethod.Get).Respond(res => new HttpResponseMessage()
            {
                Content = JsonContent.Create<PlaylistResponseBase<PlaylistItem[]>>(new PlaylistResponseBase<PlaylistItem[]>()
                {
                    data = playlist.Items.Where(x => x.AlbumId != 0).ToArray()
                }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                StatusCode = System.Net.HttpStatusCode.OK
            });

            // get playlist artists
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/playlists/*/artists").With(req => req.Method == HttpMethod.Get).Respond(res => new HttpResponseMessage()
            {
                Content = JsonContent.Create<PlaylistResponseBase<PlaylistItem[]>>(new PlaylistResponseBase<PlaylistItem[]>()
                {
                    data = playlist.Items.Where(x => x.ArtistId != 0).ToArray()
                }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                StatusCode = System.Net.HttpStatusCode.OK
            });

            // get playlist media
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/playlists/*/media").With(req => req.Method == HttpMethod.Get).Respond(res => new HttpResponseMessage()
            {
                Content = JsonContent.Create<PlaylistResponseBase<PlaylistItem[]>>(new PlaylistResponseBase<PlaylistItem[]>()
                {
                    data = playlist.Items.Where(x => x.MediaId != 0).ToArray()
                }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                StatusCode = System.Net.HttpStatusCode.OK
            });

            // get playlist playlists
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/playlists/*/playlists").With(req => req.Method == HttpMethod.Get).Respond(res => new HttpResponseMessage()
            {
                Content = JsonContent.Create<PlaylistResponseBase<PlaylistItem[]>>(new PlaylistResponseBase<PlaylistItem[]>()
                {
                    data = playlist.Items.Where(x => x.id != 0).ToArray()
                }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                StatusCode = System.Net.HttpStatusCode.OK
            });

            // get playlist users
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/playlists/*/users").With(req => req.Method == HttpMethod.Get).Respond(res => new HttpResponseMessage()
            {
                Content = JsonContent.Create<PlaylistResponseBase<PlaylistUser[]>>(new PlaylistResponseBase<PlaylistUser[]>()
                {
                    data = new PlaylistUser[]
                    {
                        new PlaylistUser()
                    }
                }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                StatusCode = System.Net.HttpStatusCode.OK
            });

            // delete playlist
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/playlists/1").With(req => req.Method == HttpMethod.Delete).Respond((res) =>
            {
                playlist = new PlaylistResponse()
                {
                    Items = new List<PlaylistItem>()
                };

                return new HttpResponseMessage()
                {
                    Content = JsonContent.Create<PlaylistResponseBase>(new PlaylistResponseBase()
                    {
                        success = true
                    }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            });

            // delete album from
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/playlists/*/albums/*").With(req => req.Method == HttpMethod.Delete).Respond((res) =>
            {
                playlist.Items = playlist.Items.Where(x => x.AlbumId == 0).ToList();

                return new HttpResponseMessage()
                {
                    Content = JsonContent.Create<PlaylistResponseBase>(new PlaylistResponseBase()
                    {
                        success = true
                    }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            });

            // delete artist from playlist
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/playlists/*/artists/*").With(req => req.Method == HttpMethod.Delete).Respond((res) =>
            {
                playlist.Items = playlist.Items.Where(x => x.ArtistId == 0).ToList();

                return new HttpResponseMessage()
                {
                    Content = JsonContent.Create<PlaylistResponseBase>(new PlaylistResponseBase()
                    {
                        success = true
                    }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            });

            // delete tracks from playlist
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/playlists/*/media/*").With(req => req.Method == HttpMethod.Delete).Respond((res) =>
            {
                playlist.Items = playlist.Items.Where(x => x.MediaId == 0).ToList();

                return new HttpResponseMessage()
                {
                    Content = JsonContent.Create<PlaylistResponseBase>(new PlaylistResponseBase()
                    {
                        success = true
                    }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            });

            // delete playlist from playlist
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/playlists/*/playlists/*").With(req => req.Method == HttpMethod.Delete).Respond((res) =>
            {
                playlist.Items = playlist.Items.Where(x => x.MediaId == 0).ToList();

                return new HttpResponseMessage()
                {
                    Content = JsonContent.Create<PlaylistResponseBase>(new PlaylistResponseBase()
                    {
                        success = true
                    }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            });

            // delete user from playlist
            this.mockHttpMessageHandler.When("https://playlist.mcnemanager.com/playlists/*/users/*").With(req => req.Method == HttpMethod.Delete).Respond((res) =>
            {
                playlist.Items = playlist.Items.Where(x => x.id == 0).ToList();

                return new HttpResponseMessage()
                {
                    Content = JsonContent.Create<PlaylistResponseBase>(new PlaylistResponseBase()
                    {
                        success = true
                    }, new MediaTypeHeaderValue("application/json"), new JsonSerializerOptions() { PropertyNamingPolicy = null }),
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            });
        }
    }
}
