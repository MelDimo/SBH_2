using ComplexObjects;
using Support;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SrvOrganizationModel
{
    internal class DBAccess
    {
        internal ObservableCollection<ComplexOrganization> GetOrganization()
        {
            ObservableCollection<ComplexOrganization> result = new ObservableCollection<ComplexOrganization>();

            using (SqlConnection con = new SqlConnection(GValues.GValues.connString))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = con;
                    command.CommandText = " SELECT org.id, org.name, org.ref_status AS refStatus, " +
                                        " 	CONVERT(XML, " +
                                        " 		(SELECT b.id, b.name, b.ref_status AS refStatus, " +
                                        " 			CONVERT(XML, " +
                                        " 				(SELECT u.id, u.name, u.ref_status AS refStatus, isPOS, isDepot " +
                                        " 				FROM unit u " +
                                        " 				WHERE u.branch = b.id " +
                                        " 				FOR XML RAW('Unit'), ROOT('ArrayOfUnit'), ELEMENTS)) " +
                                        " 		FROM branch b " +
                                        " 		WHERE b.organization = org.id " +
                                        " 		FOR XML RAW('Branch'), ROOT('ArrayOfBranch'), ELEMENTS)) " +
                                        " FROM organization org " +
                                        " FOR XML RAW('Organization'), ROOT('ArrayOfOrganization'), ELEMENTS ";

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
