using System;
using System.Text;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using LSDBUtility;
namespace DAL
{
    //t_attendanceClockGoGo
    public partial class t_attendanceClockGoGo
    {
        private MSSqlHelper DbHelperSQL = new MSSqlHelper("mainDB");

        public bool Exists(int autoid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from t_attendanceClockGoGo");
            strSql.Append(" where ");
            strSql.Append(" autoid = @autoid  ");
            SqlParameter[] parameters = {
                    new SqlParameter("@autoid", SqlDbType.Int,4)
            };
            parameters[0].Value = autoid;

            object ret = DbHelperSQL.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);
            return (int)ret == 0 ? false : true;
        }



        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.t_attendanceClockGoGo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into t_attendanceClockGoGo(");
            strSql.Append("isBiometric,id,time,createTime,date,workspotName,employmentCode,gpsLat,cardType,workspotCode,gpsLng");
            strSql.Append(") values (");
            strSql.Append("@isBiometric,@id,@time,@createTime,@date,@workspotName,@employmentCode,@gpsLat,@cardType,@workspotCode,@gpsLng");
            strSql.Append(") ");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                        new SqlParameter("@isBiometric", SqlDbType.Bit,1) ,
                        new SqlParameter("@id", SqlDbType.BigInt,8) ,
                        new SqlParameter("@time", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@createTime", SqlDbType.DateTime) ,
                        new SqlParameter("@date", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@workspotName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@employmentCode", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@gpsLat", SqlDbType.Decimal,9) ,
                        new SqlParameter("@cardType", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@workspotCode", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@gpsLng", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.isBiometric;
            parameters[1].Value = model.id;
            parameters[2].Value = model.time;
            parameters[3].Value = model.createTime;
            parameters[4].Value = model.date;
            parameters[5].Value = model.workspotName;
            parameters[6].Value = model.employmentCode;
            parameters[7].Value = model.gpsLat;
            parameters[8].Value = model.cardType;
            parameters[9].Value = model.workspotCode;
            parameters[10].Value = model.gpsLng;

            object obj = DbHelperSQL.ExecuteScalar(CommandType.Text, strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }

        }


        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.t_attendanceClockGoGo model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update t_attendanceClockGoGo set ");

            strSql.Append(" isBiometric = @isBiometric , ");
            strSql.Append(" id = @id , ");
            strSql.Append(" time = @time , ");
            strSql.Append(" createTime = @createTime , ");
            strSql.Append(" date = @date , ");
            strSql.Append(" workspotName = @workspotName , ");
            strSql.Append(" employmentCode = @employmentCode , ");
            strSql.Append(" gpsLat = @gpsLat , ");
            strSql.Append(" cardType = @cardType , ");
            strSql.Append(" workspotCode = @workspotCode , ");
            strSql.Append(" gpsLng = @gpsLng  ");
            strSql.Append(" where autoid=@autoid ");

            SqlParameter[] parameters = {
                        new SqlParameter("@autoid", SqlDbType.Int,4) ,
                        new SqlParameter("@isBiometric", SqlDbType.Bit,1) ,
                        new SqlParameter("@id", SqlDbType.BigInt,8) ,
                        new SqlParameter("@time", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@createTime", SqlDbType.DateTime) ,
                        new SqlParameter("@date", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@workspotName", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@employmentCode", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@gpsLat", SqlDbType.Decimal,9) ,
                        new SqlParameter("@cardType", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@workspotCode", SqlDbType.NVarChar,50) ,
                        new SqlParameter("@gpsLng", SqlDbType.Decimal,9)

            };

            parameters[0].Value = model.autoid;
            parameters[1].Value = model.isBiometric;
            parameters[2].Value = model.id;
            parameters[3].Value = model.time;
            parameters[4].Value = model.createTime;
            parameters[5].Value = model.date;
            parameters[6].Value = model.workspotName;
            parameters[7].Value = model.employmentCode;
            parameters[8].Value = model.gpsLat;
            parameters[9].Value = model.cardType;
            parameters[10].Value = model.workspotCode;
            parameters[11].Value = model.gpsLng;
            int rows = DbHelperSQL.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return rows > 0 ? true : false;
        }


        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int autoid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from t_attendanceClockGoGo ");
            strSql.Append(" where autoid=@autoid");
            SqlParameter[] parameters = {
                    new SqlParameter("@autoid", SqlDbType.Int,4)
            };
            parameters[0].Value = autoid;


            int rows = DbHelperSQL.ExecuteNonQuery(CommandType.Text, strSql.ToString(), parameters);
            return rows > 0 ? true : false;
        }

        /// <summary>
        /// 批量删除一批数据
        /// </summary>
        public bool DeleteList(string autoidlist)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from t_attendanceClockGoGo ");
            strSql.Append(" where ID in (" + autoidlist + ")  ");
            int rows = DbHelperSQL.ExecuteNonQuery(CommandType.Text, strSql.ToString(), null);
            return rows > 0 ? true : false;
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.t_attendanceClockGoGo GetModel(int autoid)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select autoid, isBiometric, id, time, createTime, date, workspotName, employmentCode, gpsLat, cardType, workspotCode, gpsLng  ");
            strSql.Append("  from t_attendanceClockGoGo ");
            strSql.Append(" where autoid=@autoid");
            SqlParameter[] parameters = {
                    new SqlParameter("@autoid", SqlDbType.Int,4)
            };
            parameters[0].Value = autoid;


            Model.t_attendanceClockGoGo model = null;
            DataTable dt = DbHelperSQL.ExecuteTable(CommandType.Text, strSql.ToString(), parameters);

            if (dt.Rows.Count > 0)
            {
                model = new Model.t_attendanceClockGoGo();
                if (dt.Rows[0]["autoid"].ToString() != "")
                {
                    model.autoid = int.Parse(dt.Rows[0]["autoid"].ToString());
                }
                if (dt.Rows[0]["isBiometric"].ToString() != "")
                {
                    if ((dt.Rows[0]["isBiometric"].ToString() == "1") || (dt.Rows[0]["isBiometric"].ToString().ToLower() == "true"))
                    {
                        model.isBiometric = true;
                    }
                    else
                    {
                        model.isBiometric = false;
                    }
                }
                if (dt.Rows[0]["id"].ToString() != "")
                {
                    model.id = long.Parse(dt.Rows[0]["id"].ToString());
                }
                model.time = dt.Rows[0]["time"].ToString();
                if (dt.Rows[0]["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(dt.Rows[0]["createTime"].ToString());
                }
                model.date = dt.Rows[0]["date"].ToString();
                model.workspotName = dt.Rows[0]["workspotName"].ToString();
                model.employmentCode = dt.Rows[0]["employmentCode"].ToString();
                if (dt.Rows[0]["gpsLat"].ToString() != "")
                {
                    model.gpsLat = decimal.Parse(dt.Rows[0]["gpsLat"].ToString());
                }
                model.cardType = dt.Rows[0]["cardType"].ToString();
                model.workspotCode = dt.Rows[0]["workspotCode"].ToString();
                if (dt.Rows[0]["gpsLng"].ToString() != "")
                {
                    model.gpsLng = decimal.Parse(dt.Rows[0]["gpsLng"].ToString());
                }
            }
            return model;
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataTable GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * ");
            strSql.Append(" FROM t_attendanceClockGoGo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.ExecuteTable(CommandType.Text, strSql.ToString(), null);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataTable GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * ");
            strSql.Append(" FROM t_attendanceClockGoGo ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.ExecuteTable(CommandType.Text, strSql.ToString(), null);
        }

        /// <summary>
        /// GetPage("gs_id>3", "gs_id", "order by gs_id", 2, 3, out recordCount, out pageCount);
        /// </summary>
        /// <param name="Where">"" or "gs_id>3"</param>
        /// <param name="IndexField">"gs_id"</param>
        /// <param name="OrderFields">"order by gs_id" or "order by gs_id desc"</param>
        /// <param name="PageIndex">2</param>
        /// <param name="PageSize">3</param>
        /// <param name="RecordCount">out recordCount</param>
        /// <param name="PageCount">out pageCount</param>
        /// <returns></returns>
        public DataTable GetPage(string Where, string IndexField, string OrderFields, int PageIndex, int PageSize, out int RecordCount, out int PageCount)
        {
            string SqlTablesAndWhere = string.IsNullOrEmpty(Where.Trim()) ? "t_attendanceClockGoGo" : "t_attendanceClockGoGo where " + Where;
            return DbHelperSQL.ExecutePage("*", SqlTablesAndWhere, IndexField, OrderFields, PageIndex, PageSize, out RecordCount, out PageCount, null);
        }
    }
}