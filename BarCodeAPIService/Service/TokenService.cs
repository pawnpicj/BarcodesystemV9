using System;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Barcodesystem.Request.Token;
using Barcodesystem.Respones.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BarCodeAPIService.Service
{
    public class TokenService : ITokenService
    {
        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        #region Varraible Declare

        private readonly IConfiguration configuration;
        protected SqlConnection connectionString = new();
        protected SqlConnection connectionStringSAP = new();
        private SqlCommand sqlCommand = new();
        private SqlDataAdapter sqlDataAdapter = new();
        private DataTable dataTable = new();

        #endregion

        #region Function

        protected bool CheckConnection(SqlConnection sqlConnection)
        {
            if (sqlConnection.State == ConnectionState.Open) return true;
            return false;
        }

        private GenerateTokenResponse GenerateTokenJwt(string cardCode)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(configuration.GetSection("Secret").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, cardCode),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    //new Claim(JwtRegisteredClaimNames.Email,a.Email),
                    new Claim("id", cardCode + Convert.ToString(DateTime.UtcNow.ToString()))
                }),
                Expires = DateTime.UtcNow.AddHours(2), //DateTime.UtcNow.Add(_jwtSetting.TokenLifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new GenerateTokenResponse
            {
                Token = tokenHandler.WriteToken(token),
                ValidFrom = token.ValidFrom.ToString(),
                ValidTo = token.ValidTo.ToString()
            };
        }

        #endregion

        #region Service

        public Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            connectionStringSAP.ConnectionString = configuration.GetSection("ConnectionStringSAP").Value;
            connectionString.ConnectionString = configuration.GetSection("ConnectionString").Value;
            //if (CheckConnection(connectionStringSAP) == false)
            //{
            //    connectionStringSAP.Open();
            //}
            //sqlCommand.Connection = connectionStringSAP;
            //sqlCommand.CommandType = CommandType.StoredProcedure;
            //sqlCommand.CommandText = "USP_CheckPasswordSAP";
            //sqlCommand.Parameters.AddWithValue("@cardCode", loginRequest.CardCode);
            //sqlCommand.Parameters.AddWithValue("@passWord", loginRequest.Password);
            //sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            try
            {
                //sqlDataAdapter.Fill(dataTable);
                //if (dataTable.Rows.Count > 0)
                if (1 == 1)
                {
                    sqlCommand = new SqlCommand();
                    sqlCommand.Connection = connectionString;
                    sqlCommand.CommandType = CommandType.Text;
                    sqlCommand.CommandText = "SELECT * FROM Tb_Token WHERE CardCode=@CardCode AND active=1";
                    sqlCommand.Parameters.AddWithValue("@CardCode", loginRequest.CardCode);
                    sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                    dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    //if (dataTable.Rows.Count > 0)
                    if (1 == 2)
                    {
                        var loginResponse = new LoginResponse
                        {
                            ErrorCode = 4,
                            ErrorMessage = "Not Found Already have token",
                            Token = ""
                        };
                        return Task.FromResult(loginResponse);
                    }
                    else
                    {
                        var a = GenerateTokenJwt(loginRequest.CardCode);
                        sqlCommand = new SqlCommand();
                        sqlCommand.Connection = connectionString;
                        sqlCommand.CommandType = CommandType.Text;
                        sqlCommand.CommandText =
                            "INSERT INTO Tb_Token(CardCode,Token,active,CreateDate,[Update],ExpireDate) " +
                            "values(@CardCode,@Token,@active,@createDate,@updateDate,@expireDate)";
                        sqlCommand.Parameters.AddWithValue("@CardCode", loginRequest.CardCode);
                        sqlCommand.Parameters.AddWithValue("@Token", a.Token);
                        sqlCommand.Parameters.AddWithValue("@active", true);
                        sqlCommand.Parameters.AddWithValue("@createDate",
                            Convert.ToDateTime(a.ValidFrom).ToLocalTime());
                        sqlCommand.Parameters.AddWithValue("@updateDate", DateTime.UtcNow);
                        sqlCommand.Parameters.AddWithValue("@expireDate", Convert.ToDateTime(a.ValidTo).ToLocalTime());
                        sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                        dataTable = new DataTable();
                        sqlDataAdapter.Fill(dataTable);
                        var dt1 = dataTable;
                        var loginResponse = new LoginResponse
                        {
                            ErrorCode = 0,
                            ErrorMessage = "",
                            Token = a.Token
                        };
                        return Task.FromResult(loginResponse);
                    }
                }

                {
                    var loginResponse = new LoginResponse
                    {
                        ErrorCode = 4,
                        ErrorMessage = "Not Found CardCode",
                        Token = ""
                    };
                    return Task.FromResult(loginResponse);
                }
            }
            catch (Exception ex)
            {
                var loginResponse = new LoginResponse
                {
                    ErrorCode = ex.HResult,
                    ErrorMessage = ex.Message,
                    Token = ""
                };
                return Task.FromResult(loginResponse);
            }
        }

        public Task<RefreshResponse> RefrestAsync(RefreshRequest refreshRequest)
        {
            connectionString.ConnectionString = configuration.GetSection("ConnectionString").Value;
            sqlCommand = new SqlCommand();
            sqlCommand.Connection = connectionString;
            sqlCommand.CommandType = CommandType.Text;
            sqlCommand.CommandText = "SELECT * FROM Tb_Token WHERE CardCode=@CardCode AND Token=@token AND active=1";
            sqlCommand.Parameters.AddWithValue("@CardCode", refreshRequest.CardCode);
            sqlCommand.Parameters.AddWithValue("@token", refreshRequest.Token);
            sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            dataTable = new DataTable();
            sqlDataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                var expiredate = dataTable.Rows[0]["ExpireDate"].ToString();
                if (DateTime.UtcNow.ToLocalTime() < Convert.ToDateTime(expiredate))
                {
                    return Task.FromResult(new RefreshResponse
                    {
                        ErrorCode = 999,
                        ErrorMessage = "Token not expire",
                        Token = ""
                    });
                }

                var a = GenerateTokenJwt(refreshRequest.CardCode);
                sqlCommand = new SqlCommand();
                sqlCommand.Connection = connectionString;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.CommandText =
                    "UPDATE Tb_Token SET Token=@token,CreateDate=@createDate,ExpireDate=@expireDate Where CardCode=@cardCode";
                sqlCommand.Parameters.AddWithValue("@token", a.Token);
                sqlCommand.Parameters.AddWithValue("@createDate", Convert.ToDateTime(a.ValidFrom).ToLocalTime());
                sqlCommand.Parameters.AddWithValue("@expireDate", Convert.ToDateTime(a.ValidTo).ToLocalTime());
                sqlCommand.Parameters.AddWithValue("@cardCode", refreshRequest.CardCode);
                sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);
                return Task.FromResult(new RefreshResponse
                {
                    ErrorCode = 0,
                    ErrorMessage = "",
                    Token = a.Token
                });
            }

            return Task.FromResult(new RefreshResponse
            {
                ErrorCode = 9999,
                ErrorMessage = "invalid Token Key or CardCode",
                Token = ""
            });
        }

        #endregion
    }
}