using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Services.Protocols;

namespace WS
{
    /// <summary>
    /// Descrição resumida de WebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService : System.Web.Services.WebService
    {
        public WebService()
        {

        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Olá, Mundo";
        }
        [WebMethod]
        public DataTable Get() //read
        {
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("Select * FROM USERS_T"))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter())
                    {
                        cmd.Connection = con;
                        da.SelectCommand = cmd;
                        using (DataTable dt = new DataTable())
                        {
                            dt.TableName = "customer";
                            da.Fill(dt);
                            return dt;
                        }
                    }
                }
            }
        }
        [WebMethod]
        public string Update(int id, string nome) //update
        {
            string result = "";
            string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
            try
            {
                using (SqlConnection con = new SqlConnection(constr))
                {
                    using (SqlCommand cmd = new SqlCommand($"UPDATE USERS_T SET nome = '{nome}' WHERE id = {id}"))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter())
                        {
                            cmd.Connection = con;
                            da.SelectCommand = cmd;
                            using (DataTable dt = new DataTable())
                            {
                                dt.TableName = "customer";
                                da.Fill(dt);
                            }
                        }
                    }
                }
                result = "Concluido!";
            }
            catch (Exception ex)
            {
                result = $"Erro: {ex}";
            }

            return result;
        }
        [WebMethod]
        public string Insert(int Id, string Nome) //insert
        {
            string result = "";

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["constr"].ConnectionString);
            SqlCommand cmd = new SqlCommand();

            string Query = "insert into USERS_T values(' " + @Id+" ', '"+@Nome+"')";
            cmd = new SqlCommand(Query, con);

            con.Open();
            cmd.ExecuteNonQuery();
            result = "Sucesso!";
            con.Close();

            return result;
        }


    }
}
