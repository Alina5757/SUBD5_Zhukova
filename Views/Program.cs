using BusinessLogic.BusinessLogics;
using Contracts.BusinessLogicsContracts;
using Contracts.StoragesContracts;
using DataBaseImplement.Implements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace Views
{
    static class Program
    {
        private static IUnityContainer container = null;
        public static IUnityContainer Container { get { if (container == null) { container = BuildUnityContainer(); } return container; } }

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(Container.Resolve<FormMain>());
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<IGSMStorage, GSMStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISellStorage, SellStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISmenaStorage, SmenaStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkerStorage, WorkerStorage>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkerGSMStorage, WorkerGSMStorage>(new HierarchicalLifetimeManager());

            currentContainer.RegisterType<IGSMLogic, GSMLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISellLogic, SellLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<ISmenaLogic, SmenaLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkerLogic, WorkerLogic>(new HierarchicalLifetimeManager());
            currentContainer.RegisterType<IWorkerGSMLogic, WorkerGSMLogic>(new HierarchicalLifetimeManager());

            return currentContainer;
        }
    }
}
