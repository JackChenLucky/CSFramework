using System;
using Service.Common.Domain;   
using Service.Common.PersistBroker;

namespace Service.Common.DomainDataProvider
{
	/// <summary>
	/// DomainDataProviderManager ��ժҪ˵����
	/// </summary>
	public class DomainDataProviderManager
	{
		public DomainDataProviderManager()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		public static IDomainDataProvider  DomainDataProvider()
		{
			return DomainDataProviderManager.DomainDataProvider(null, new System.Globalization.CultureInfo("en-US", false)); 
		}

		public static IDomainDataProvider  DomainDataProvider(System.Globalization.CultureInfo  cultureInfo)
		{
			return DomainDataProviderManager.DomainDataProvider(null, cultureInfo); 
		}

		public static IDomainDataProvider  DomainDataProvider(IPersistBroker persistBroker, System.Globalization.CultureInfo  cultureInfo)
		{
			if (cultureInfo == null)
			{
				cultureInfo = new System.Globalization.CultureInfo("en-US", false);
			}
			if (persistBroker == null)//Ŀǰֻ����sqlserver��privider
			{
                return new SQLDomainDataProvider(persistBroker, cultureInfo);
			}
			else
			{
				return new SQLDomainDataProvider(persistBroker, cultureInfo); 
			}
		}

        //public static IDomainDataProvider DomainDataProvider(string ConnectDB)
        //{
        //    return new SQLDomainDataProvider(PersistBrokerFactory.PersistBroker(ConnectDB), new System.Globalization.CultureInfo("en-US", false));
        //}
	}
}
