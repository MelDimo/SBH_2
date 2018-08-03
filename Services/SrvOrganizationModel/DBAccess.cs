using com.sbh.dll.support;
using com.sbh.dto.complexobjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace com.sbh.srv.implementations
{
    internal class DBAccess
    {
        internal ObservableCollection<ComplexOrganization> GetOrganization()
        {
            ObservableCollection<ComplexOrganization> result = new ObservableCollection<ComplexOrganization>();

            using (SqlConnection con = new SqlConnection(GValues.connString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = con;
                    command.CommandText = " SELECT org.Id, org.Name, org.ref_status AS RefStatus, " +
                                        " 	CONVERT(XML, " +
                                        " 		(SELECT b.Id, b.Name, b.ref_status AS RefStatus, " +
                                        " 			CONVERT(XML, " +
                                        " 				(SELECT u.Id, u.Name, u.ref_status AS RefStatus, IsPOS, IsDepot " +
                                        " 				FROM unit u " +
                                        " 				WHERE u.branch = b.id " +
                                        " 				FOR XML RAW('Unit'), ROOT('ArrayOfUnit'), ELEMENTS)) " +
                                        " 		FROM branch b " +
                                        " 		WHERE b.organization = org.id " +
                                        " 		FOR XML RAW('ComplexBranch'), ROOT('ArrayOfComplexBranch'), ELEMENTS)) " +
                                        " FROM organization org " +
                                        " FOR XML RAW('ComplexOrganization'), ROOT('ArrayOfComplexOrganization'), ELEMENTS ";

                    XmlReader reader = command.ExecuteXmlReader();
                    while (reader.Read())
                    {
                        result = XMLToFromObject.XMLToObject<ObservableCollection<ComplexOrganization>>(reader.ReadOuterXml());
                    }
                }
            }
            return result;
        }
    }
}
