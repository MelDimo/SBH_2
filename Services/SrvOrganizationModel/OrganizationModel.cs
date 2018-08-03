using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using com.sbh.dto.complexobjects;
using com.sbh.dto.simpleobjects;
using com.sbh.srv.interfaces;

namespace com.sbh.srv.implementations
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class OrganizationModel : IOrganizationModel
    {
        readonly DBAccess dbAccess = null;

        private ICallBack callback = null;
        //private ObservableCollection<ComplexOrganization> complexOrganizations;
        //private Dictionary<string, IOrganizationModelCallback> clients;

        public OrganizationModel()
        {
            dbAccess = new DBAccess();
            //complexOrganizations = new ObservableCollection<ComplexOrganization>();
            //clients = new Dictionary<string, IOrganizationModelCallback>();
        }

        public void GetOrganization()
        {
            MSG msg = new MSG();
            try
            {
                msg.Obj = dbAccess.GetOrganization();
                msg.Code = CODES.SUCCESS;
            }
            catch (Exception exc)
            {
                msg.Code = CODES.ERROR;
                msg.Text = exc.Message;
            }

            callback.FromOrganizationModel(msg);
        }

        public void AddOrganization(Organization organization)
        {
            callback.FromOrganizationModel(new MSG { Code = CODES.SUCCESS, Obj = null, Text = "All fine" });
        }


    }
}
