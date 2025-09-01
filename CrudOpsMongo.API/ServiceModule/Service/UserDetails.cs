using CrudOpsMongo.API.Models;
using CrudOpsMongo.API.ServiceModule.Interface;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Net;
using System.Reflection;

namespace CrudOpsMongo.API.ServiceModule.Service
{
    public class UserDetails : IUserDetails
    {
        private readonly IConfiguration _config;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<UserDetailModel> _mongoCollection;
        public UserDetails(IConfiguration config)
        {
            _config = config;
            _mongoClient = new MongoClient(_config[key: "DBSettings:ConnectionString"]);
            var _mongoDB = _mongoClient.GetDatabase(_config[key: "DBSettings:DatabaseName"]);
            _mongoCollection = _mongoDB.GetCollection<UserDetailModel>(_config[key: "DBSettings:CollectionName"]);
        }
        public async Task<ResponseMessage> InsertUserDetailsAsync(UserDetailModel model)
        {
            ResponseMessage response = new();
            try
            {
                model.IsActive = true;
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = 99;
                await _mongoCollection.InsertOneAsync(model);
                response.IsSuccess = true;
                response.StatusCode = (int)HttpStatusCode.Created;
                response.Message = "User details inserted successfully";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = ex.Message;
            }
            return response;
        }
        public async Task<ResponseMessage> GetUserDetailsAsync()
        {
            ResponseMessage response = new();
            try
            {
                response.IsSuccess = true;
                response.StatusCode = (int)HttpStatusCode.OK;

                response.Data = new List<UserDetailModel>();
                response.Data = await _mongoCollection.Find(x => true).ToListAsync();
                if (response.Data.Count == 0)
                {
                    response.Message = "No Records found";
                }
                else
                {
                    response.Message = "Records fetched successfully";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception Occured: " + ex.Message;
            }
            return response;
        }
        public async Task<ResponseMessage> GetUserDetailsByIdAsync(string? id)
        {
            ResponseMessage response = new();
            try
            {
                response.IsSuccess = true;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Data = await _mongoCollection.Find(x => x.Id == id).ToListAsync();

                if (response.Data.Count == 0)
                {
                    response.Message = "Please enter valid Id";
                }
                else
                {
                    response.Message = "Record fetched successfully.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception Occured: " + ex.Message;
            }
            return response;
        }
        public async Task<ResponseMessage> GetUserDetailsByNameAsync(string? name)
        {
            ResponseMessage response = new();
            try
            {
                response.IsSuccess = true;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.Data = await _mongoCollection.Find(x => (x.FirstName == name) || (x.LastName == name)).ToListAsync();

                if (response.Data.Count == 0)
                {
                    response.Message = "Record with given name does not exists. Please enter valid name";
                }
                else
                {
                    response.Message = "Record fetched successfully.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception Occured: " + ex.Message;
            }
            return response;
        }

        public async Task<ResponseMessage> UpdateUserDetailsByIdAsync(UserDetailModel model)
        {
            ResponseMessage response = new();
            try
            {
                ResponseMessage response1 = await GetUserDetailsByIdAsync(model.Id);
                model.CreatedDate = response1.Data[0].CreatedDate;
                model.CreatedBy = response1.Data[0].CreatedBy;
                model.UpdatedDate = DateTime.Now;
                model.UpdatedBy = 88;
                var result = await _mongoCollection.ReplaceOneAsync(x => x.Id == model.Id, model);
                if (!result.IsAcknowledged)     // "result.IsAcknowledged" remains true if any update occurs in the table.
                {
                    response.IsSuccess = false;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Id does not exists / Updation not occured.";
                }
                else
                {
                    response.IsSuccess = true;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Record updated successfully";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception Occured: " + ex.Message;
            }
            return response;
        }
        public async Task<ResponseMessage> UpdateUserSalaryByIdAsync(UserDetailModel model)
        {
            ResponseMessage response = new();
            try
            {
                var filter = new BsonDocument().Add("Salary", model.Salary).Add("UpdatedDate", DateTime.Now).Add("UpdatedBy", 188);
                var updateSingleRecord = new BsonDocument("$set", filter);
                var result = await _mongoCollection.UpdateOneAsync(x => x.Id == model.Id, updateSingleRecord);
                if (!result.IsAcknowledged)     // "result.IsAcknowledged" remains true if any update occurs in the table.
                {
                    response.IsSuccess = false;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Id does not exists / Updation not occured.";
                }
                else
                {
                    response.IsSuccess = true;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Record updated successfully";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception Occured: " + ex.Message;
            }
            return response;
        }
        public async Task<ResponseMessage> DeleteUserByIdAsync(string? id)
        {
            ResponseMessage response = new();
            try
            {
                var result = await _mongoCollection.DeleteOneAsync(x => x.Id == id);
                if (!result.IsAcknowledged)
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Id does not exists / Record not deleted.";
                }
                else
                {
                    response.IsSuccess = true;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Record deleted successfully by id";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception Occured: " + ex.Message;
            }
            return response;
        }
        public async Task<ResponseMessage> DeleteUserByGenderAsync(string? gender)
        {
            ResponseMessage response = new();
            try
            {
                var result = await _mongoCollection.DeleteManyAsync(x => x.Gender == gender);
                if (!result.IsAcknowledged)
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Gender entered by user does not exists / Record not deleted.";
                }
                else
                {
                    response.IsSuccess = true;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Record(s) deleted successfully";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception Occured: " + ex.Message;
            }
            return response;
        }
        public async Task<ResponseMessage> DeleteAllUsersAsync()
        {
            ResponseMessage response = new();
            try
            {
                var result = await _mongoCollection.DeleteManyAsync(x=> true);  //means no condition need to be applied and perform operation on entire collection. 
                if (!result.IsAcknowledged)
                {
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Record not deleted.";
                }
                else
                {
                    response.IsSuccess = true;
                    response.StatusCode = (int)HttpStatusCode.OK;
                    response.Message = "Record(s) deleted successfully";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                response.Message = "Exception Occured: " + ex.Message;
            }
            return response;
        }
    }
}
