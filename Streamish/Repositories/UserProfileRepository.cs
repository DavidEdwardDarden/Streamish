//using Microsoft.Extensions.Configuration;
//using Streamish.Models;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Streamish.Repositories
//{
//    public class UserProfileRepository : BaseRepository, IUserProfileRespository
//    {
//        public UserProfileRepository(IConfiguration configuration) : base(configuration) { }

//        public List<UserProfile> GetAll()
//        {
//            using (var conn = Connection)
//            {
//                conn.Open();
//                using (var cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = @"
//               SELECT v.Id, v.Title, v.Description, v.Url, v.DateCreated, v.UserProfileId,

//                      up.Name, up.Email, up.DateCreated AS UserProfileDateCreated,
//                      up.ImageUrl AS UserProfileImageUrl
                        
//                 FROM UserProfile v 
//                      JOIN UserProfile up ON v.UserProfileId = up.Id
//             ORDER BY DateCreated
//            ";

//                    var reader = cmd.ExecuteReader();

//                    var videos = new List<UserProfile>();
//                    while (reader.Read())
//                    {
//                        videos.Add(new UserProfile()
//                        {
//                            Id = DbUtils.GetInt(reader, "Id"),
//                            Title = DbUtils.GetString(reader, "Title"),
//                            Description = DbUtils.GetString(reader, "Description"),
//                            Url = DbUtils.GetString(reader, "Url"),
//                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
//                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
//                            UserProfile = new UserProfile()
//                            {
//                                Id = DbUtils.GetInt(reader, "UserProfileId"),
//                                Name = DbUtils.GetString(reader, "Name"),
//                                Email = DbUtils.GetString(reader, "Email"),
//                                DateCreated = DbUtils.GetDateTime(reader, "UserProfileDateCreated"),
//                                ImageUrl = DbUtils.GetString(reader, "UserProfileImageUrl"),
//                            },
//                        });
//                    }

//                    reader.Close();

//                    return videos;
//                }
//            }
//        }

//        public UserProfile GetById(int id)
//        {
//            using (var conn = Connection)
//            {
//                conn.Open();
//                using (var cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = @"
//                          SELECT v.Title, v.Description, v.Url, v.DateCreated, v.UserProfileId, up.Id, up.Name, up.Email, up.ImageUrl, up.DateCreated
//                            FROM UserProfile v
//                            LEFT JOIN UserProfile up ON v.UserProfileId = up.Id
//                            WHERE v.Id = @Id";

//                    DbUtils.AddParameter(cmd, "@Id", id);

//                    var reader = cmd.ExecuteReader();

//                    UserProfile video = null;
//                    UserProfile userprofile = null;
//                    if (reader.Read())
//                    {

//                        video = new UserProfile()
//                        {
//                            Id = id,
//                            Title = DbUtils.GetString(reader, "Title"),
//                            Description = DbUtils.GetString(reader, "Description"),
//                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
//                            Url = DbUtils.GetString(reader, "Url"),
//                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
//                            UserProfile = new UserProfile()
//                            {
//                                Id = DbUtils.GetInt(reader, "UserProfileId"),
//                                Name = DbUtils.GetString(reader, "Name"),
//                                Email = DbUtils.GetString(reader, "Email"),
//                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
//                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
//                            }
//                        };
//                    }

//                    reader.Close();

//                    return video;
//                }
//            }
//        }

//        public void Add(UserProfile video)
//        {
//            using (var conn = Connection)
//            {
//                conn.Open();
//                using (var cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = @"
//                        INSERT INTO UserProfile (Title, Description, DateCreated, Url, UserProfileId)
//                        OUTPUT INSERTED.ID
//                        VALUES (@Title, @Description, @DateCreated, @Url, @UserProfileId)";

//                    DbUtils.AddParameter(cmd, "@Title", video.Title);
//                    DbUtils.AddParameter(cmd, "@Description", video.Description);
//                    DbUtils.AddParameter(cmd, "@DateCreated", video.DateCreated);
//                    DbUtils.AddParameter(cmd, "@Url", video.Url);
//                    DbUtils.AddParameter(cmd, "@UserProfileId", video.UserProfileId);

//                    video.Id = (int)cmd.ExecuteScalar();
//                }
//            }
//        }

//        public void Update(UserProfile video)
//        {
//            using (var conn = Connection)
//            {
//                conn.Open();
//                using (var cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = @"
//                        UPDATE UserProfile
//                           SET Title = @Title,
//                               Description = @Description,
//                               DateCreated = @DateCreated,
//                               Url = @Url,
//                               UserProfileId = @UserProfileId
//                         WHERE Id = @Id";

//                    DbUtils.AddParameter(cmd, "@Title", video.Title);
//                    DbUtils.AddParameter(cmd, "@Description", video.Description);
//                    DbUtils.AddParameter(cmd, "@DateCreated", video.DateCreated);
//                    DbUtils.AddParameter(cmd, "@Url", video.Url);
//                    DbUtils.AddParameter(cmd, "@UserProfileId", video.UserProfileId);
//                    DbUtils.AddParameter(cmd, "@Id", video.Id);

//                    cmd.ExecuteNonQuery();
//                }
//            }
//        }

//        public void Delete(int id)
//        {
//            using (var conn = Connection)
//            {
//                conn.Open();
//                using (var cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = "DELETE FROM UserProfile WHERE Id = @Id";
//                    DbUtils.AddParameter(cmd, "@id", id);
//                    cmd.ExecuteNonQuery();
//                }
//            }
//        }

//        public List<UserProfile> GetAllWithComments()
//        {
//            using (var conn = Connection)
//            {
//                conn.Open();
//                using (var cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = @"
//                SELECT v.Id AS UserProfileId, v.Title, v.Description, v.Url, 
//                       v.DateCreated AS UserProfileDateCreated, v.UserProfileId As UserProfileUserProfileId,

//                       up.Name, up.Email, up.DateCreated AS UserProfileDateCreated,
//                       up.ImageUrl AS UserProfileImageUrl,
                        
//                       c.Id AS CommentId, c.Message, c.UserProfileId AS CommentUserProfileId
//                  FROM UserProfile v 
//                       JOIN UserProfile up ON v.UserProfileId = up.Id
//                       LEFT JOIN Comment c on c.UserProfileId = v.id
//             ORDER BY  v.DateCreated
//            ";

//                    var reader = cmd.ExecuteReader();

//                    var videos = new List<UserProfile>();
//                    while (reader.Read())
//                    {
//                        var videoId = DbUtils.GetInt(reader, "UserProfileId");

//                        var existingUserProfile = videos.FirstOrDefault(p => p.Id == videoId);
//                        if (existingUserProfile == null)
//                        {
//                            existingUserProfile = new UserProfile()
//                            {
//                                Id = videoId,
//                                Title = DbUtils.GetString(reader, "Title"),
//                                Description = DbUtils.GetString(reader, "Description"),
//                                DateCreated = DbUtils.GetDateTime(reader, "UserProfileDateCreated"),
//                                Url = DbUtils.GetString(reader, "Url"),
//                                UserProfileId = DbUtils.GetInt(reader, "UserProfileUserProfileId"),
//                                UserProfile = new UserProfile()
//                                {
//                                    Id = DbUtils.GetInt(reader, "UserProfileUserProfileId"),
//                                    Name = DbUtils.GetString(reader, "Name"),
//                                    Email = DbUtils.GetString(reader, "Email"),
//                                    DateCreated = DbUtils.GetDateTime(reader, "UserProfileDateCreated"),
//                                    ImageUrl = DbUtils.GetString(reader, "UserProfileImageUrl"),
//                                },
//                                Comments = new List<Comment>()
//                            };

//                            videos.Add(existingUserProfile);
//                        }

//                        if (DbUtils.IsNotDbNull(reader, "CommentId"))
//                        {
//                            existingUserProfile.Comments.Add(new Comment()
//                            {
//                                Id = DbUtils.GetInt(reader, "CommentId"),
//                                Message = DbUtils.GetString(reader, "Message"),
//                                UserProfileId = videoId,
//                                UserProfileId = DbUtils.GetInt(reader, "CommentUserProfileId")
//                            });
//                        }
//                    }


//                    reader.Close();

//                    return videos;
//                }
//            }
//        }

//        public UserProfile GetUserProfileByIdWithComments(int id)
//        {
//            using (var conn = Connection)
//            {
//                conn.Open();
//                using (var cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = @"
//                          SELECT v.Title, v.Description, v.Url, v.DateCreated, v.UserProfileId, 
//                            up.Id, up.Name, up.Email, up.ImageUrl, up.DateCreated,
//                            c.Id AS CommentId, c.Message, c.UserProfileId AS CommentUserProfileId
//                            FROM UserProfile v
//                            LEFT JOIN UserProfile up ON v.UserProfileId = up.Id
//                            LEFT JOIN Comment c on c.UserProfileId = v.id
//                            WHERE v.Id = @Id";

//                    DbUtils.AddParameter(cmd, "@Id", id);

//                    var reader = cmd.ExecuteReader();

//                    UserProfile video = null;
//                    UserProfile userprofile = null;
//                    if (reader.Read())
//                    {

//                        video = new UserProfile()
//                        {
//                            Id = id,
//                            Title = DbUtils.GetString(reader, "Title"),
//                            Description = DbUtils.GetString(reader, "Description"),
//                            DateCreated = DbUtils.GetDateTime(reader, "DateCreated"),
//                            Url = DbUtils.GetString(reader, "Url"),
//                            UserProfileId = DbUtils.GetInt(reader, "UserProfileId"),
//                            UserProfile = new UserProfile()
//                            {
//                                Id = DbUtils.GetInt(reader, "UserProfileId"),
//                                Name = DbUtils.GetString(reader, "Name"),
//                                Email = DbUtils.GetString(reader, "Email"),
//                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
//                                DateCreated = DbUtils.GetDateTime(reader, "DateCreated")
//                            }
//                        };
//                    }

//                    reader.Close();

//                    return video;
//                }
//            }
//        }

//        public void Add(Video video)
//        {
//            throw new NotImplementedException();
//        }

//        List<Video> IUserProfileRespository.GetAll()
//        {
//            throw new NotImplementedException();
//        }

//        List<Video> IUserProfileRespository.GetAllWithComments()
//        {
//            throw new NotImplementedException();
//        }

//        Video IUserProfileRespository.GetById(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public Video GetVideoByIdWithComments(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public void Update(Video video)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
