namespace Capstone_Project_441101_2223
{
    class ProjectManagerMenu : ConsoleMenu
    {
        private ProjectManager _manager;

        public ProjectManagerMenu(ProjectManager manager)
        {
            _manager = manager;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new AddNewProjectMenu(_manager));
            if (_manager.Projects.Count > 0)
            {
                _menuItems.Add(new SelectExistingProjectsMenu(_manager));
                _menuItems.Add(new RemoveProjectsMenu(_manager));
                //_menuItems.Add(new PortfolioTransactionsSummaryMenuItem(_manager));
                _menuItems.Add(new PortfolioSummaryMenuItem(_manager));
            }
            _menuItems.Add(new LoadFromFileMenu(_manager));
            _menuItems.Add(new ExitMenuItem(this));
        }

        public override string MenuText()
        {
            return "\nProject Manager Menu";
        }
    }

    class RemoveProjectsMenu : ConsoleMenu
    {
        public ProjectManager _manager;

        public RemoveProjectsMenu(ProjectManager manager)
        {
            _manager = manager;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            
            foreach (Project project in _manager.Projects)
            {
                if (project.TypeCode == 'L' || project.TypeCode == 'R')
                {
                    _menuItems.Add(new RemoveProjectsMenuItem(project, _manager));
                }
            }
            _menuItems.Add(new ExitMenuItem(this));
        }

        public override string MenuText()
        {
            return "Remove Existing Project";
        }

        public override void Select()
        {
            base.Select();
        }
    }

    class LoadFromFileMenu : ConsoleMenu
    {
        public ProjectManager _manager;

        public LoadFromFileMenu(ProjectManager manager)
        {
            _manager = manager;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new LoadTXTMenuItem(_manager));
            _menuItems.Add(new LoadTMLMenuItem(_manager));
            _menuItems.Add(new ExitMenuItem(this));
        }

        public override string MenuText()
        {
            return "File Loading Menu";
        }
    }

    class SelectExistingProjectsMenu : ConsoleMenu
    {
        private ProjectManager _manager;

        public SelectExistingProjectsMenu(ProjectManager manager)
        {
            _manager = manager;

        }

        public override string MenuText()
        {
            return "Select Existing Projects";
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            foreach (Project project in _manager.Projects)
            {
                if (project.TypeCode == 'L' || project.TypeCode == 'R')
                {
                    _menuItems.Add(new SelectedProjectsMenu(project, _manager));
                }        
            }

            _menuItems.Add(new ExitMenuItem(this));
        }
    }

    class SelectedProjectsMenu : ConsoleMenu
    {
        private Project _project;
        private ProjectManager _manager;

        public SelectedProjectsMenu(Project project, ProjectManager manager)
        {
            _project = project;
            _manager = manager;
        }

        public override string MenuText()
        {
            return $"Project ID : {_project.ProjectID}";
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new AddTransactionMenu(_project, _manager));
            _menuItems.Add(new DisplaySalesMenuItem(_project, _manager));
            _menuItems.Add(new DisplayPurchasesMenu(_project, _manager));
            _menuItems.Add(new DisplayProjectSummaryMenu(_project, _manager));
            //_menuItems.Add(new RemoveProjectsMenuItem(_project, _manager));
            _menuItems.Add(new ExitMenuItem(this));
        }


    }

    class AddTransactionMenu : ConsoleMenu
    {
        private Project _project;
        ProjectManager _manager;

        public AddTransactionMenu(Project project, ProjectManager manager)
        {
            _manager = manager;
            _project = project;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new AddPurchaseMenuItem(_project, _manager));
            _menuItems.Add(new AddSaleMenuItem(_project, _manager));
            _menuItems.Add(new ExitMenuItem(this));
        }

        public override string MenuText()
        {
            return "Add New Transaction Menu";
        }
    }

    class AddNewProjectMenu : ConsoleMenu
    {
        
        ProjectManager _manager;

        public AddNewProjectMenu(ProjectManager manager)
        {
            _manager = manager;
        }

        public override void CreateMenu()
        {
            _menuItems.Clear();
            _menuItems.Add(new NewBuildMenuItem(_manager));
            _menuItems.Add(new RenovationMenuItem(_manager));
            _menuItems.Add(new ExitMenuItem(this));
        }

        public override string MenuText()
        {
            return "Add New Project Menu";
        }
    }

    class DisplaySalesMenuItem : MenuItem
    {
        private Project _project;
        private ProjectManager _manager;

        public DisplaySalesMenuItem(Project project, ProjectManager manager)
        {
            _manager = manager;
            _project = project;
        }

        public override string MenuText()
        {
            return "Display Sales Summary";
        }

        public override void Select()
        {
            int j = 0;
           
            for (int i = 0; i < _manager.Projects.Count; i++)
            {
                if (_project.ProjectID == _manager.Projects[i].ProjectID)
                {
                    if (_manager.Projects[i].TypeCode == 'S')
                    {
                        Console.WriteLine($"\nSale {j} : {_manager.Projects[i].Transaction}\n");
                        j++;
                    }
                }
            }
            
        }
    }

    class DisplayPurchasesMenu : MenuItem
    {
        private Project _project;
        private ProjectManager _manager;

        public DisplayPurchasesMenu(Project project, ProjectManager manager)
        {
            _manager = manager;
            _project = project;
        }

        public override string MenuText()
        {
            return "Display Purchases Summary";
        }

        public override void Select()
        {
            int j = 0;

            for (int i = 0; i < _manager.Projects.Count; i++)
            {
                if (_project.ProjectID == _manager.Projects[i].ProjectID)
                {
                    if (_manager.Projects[i].TypeCode == 'P' || _manager.Projects[i].TypeCode == 'L'|| _manager.Projects[i].TypeCode == 'R')
                    {
                        Console.WriteLine($"\nPurchase {j} ({_manager.Projects[i].TypeCode}) : {_manager.Projects[i].Transaction}\n");
                        j++;
                    }
                }
            }

        }
    }
    


    class DisplayProjectSummaryMenu : MenuItem
    {
        private Project _project;
        private ProjectManager _manager;

        public DisplayProjectSummaryMenu(Project project, ProjectManager manager)
        {
            _manager = manager;
            _project = project;
        }

        public override string MenuText()
        {
            return "Display Project Summary";
        }

        public override void Select()
        {
            Console.WriteLine(_project.Summary());
        }
    }

    class PortfolioTransactionsSummaryMenuItem : MenuItem
    {
        private ProjectManager _manager;

        public PortfolioTransactionsSummaryMenuItem(ProjectManager manager)
        {
            _manager = manager;
        }

        public override string MenuText()
        {
            return "View Portfolio Transactions Summary";
        }

        public override void Select()
        {
            foreach(Project project in _manager.Projects)
            {
                Console.WriteLine(project.ToString());
            }
        }
    }

    
    class PortfolioSummaryMenuItem : MenuItem
    {
        private ProjectManager _manager;

        public PortfolioSummaryMenuItem(ProjectManager manager)
        {
            _manager = manager;
        }

        public override string MenuText()
        {
            return "View Portfolio Summary";
        }

        public override void Select()
        {
            float TotalSales = 0;
            float TotalPurchasing = 0;
            float TaxRefund = 0;
            float Profit = 0;

            foreach (Project project in _manager.Projects)
            {
                if (project.TypeCode == 'L' || project.TypeCode == 'R')
                {
                    Console.WriteLine(project.Summary() + "\n");
                }
                    
                TotalSales += project.TotalSales();
                TotalPurchasing += project.TotalPurchase();
                TaxRefund += project.TaxRefund();
                Profit += project.Profit();
            }
            Console.WriteLine($"\nPurchases: {TotalPurchasing}, Sales: {TotalSales}, Refund : {TaxRefund}, Profit : {Profit}\n");
        }
    }

    class AddPurchaseMenuItem : MenuItem
    {
        private Project _project;
        private ProjectManager _manager;


        public AddPurchaseMenuItem(Project project, ProjectManager manager)
        {
            _manager = manager;
            _project = project;
        }

        public override string MenuText()
        {
            return "New Purchase";
        }

        public override void Select()
        {
             
            float price;

            while (true)
            {
                Console.WriteLine("\nPlease enter the purchase amount");
                if (!float.TryParse(Console.ReadLine(), out price) || price < 0)
                {
                    Console.WriteLine("\nError : Invalid Price");
                }
                else
                {
                    break;
                }
                
            }

            try
            {
                Project project = new Project(_project.ProjectID, 'P', price);
                _manager.AddProject(project);
                Console.WriteLine(project.ToString());

            }
            catch { }

        }
    }


    class AddSaleMenuItem : MenuItem
    {
        private Project _project;
        private ProjectManager _manager;


        public AddSaleMenuItem(Project project, ProjectManager manager)
        {
            _manager = manager;
            _project = project;
        }

        public override string MenuText()
        {
            return "New Sale";
        }

        public override void Select()
        {

            float price;

            while (true)
            {
                Console.WriteLine("\nPlease enter the sale amount");
                if (!float.TryParse(Console.ReadLine(), out price) || price < 0)
                {
                    Console.WriteLine("\nError : Invalid Price");
                }
                else
                {
                    break;

                }
                
            }

            try
            {
                Project project = new Project(_project.ProjectID, 'S', price);
                _manager.AddProject(project);
                Console.WriteLine(project.ToString());

            }
            catch { }

        }
    }

    class RemoveProjectsMenuItem : MenuItem
    {
        private Project _project;
        private ProjectManager _manager;

        public RemoveProjectsMenuItem(Project project, ProjectManager manager)
        {
            _project = project;
            _manager = manager;
        }

        public override string MenuText()
        {
            return $"Remove Project ID : { _project.ProjectID}";

        }

        public override void Select()
        {
            for (int i = 0; i < _manager.Projects.Count; i++)
            {
                if (_project.ProjectID == _manager.Projects[i].ProjectID)
                {
                    _manager.Projects.RemoveAt(i);
                }
            }

            for (int i = 0; i < _manager.Projects.Count; i++)
            {
                if (_project.ProjectID == _manager.Projects[i].ProjectID)
                {
                    _manager.Projects.RemoveAt(i);
                }
            }

            //EXIT IF POSSIBLE

        }
    }

    class LoadTMLMenuItem : MenuItem
    {
        private ProjectManager _manager;

        public LoadTMLMenuItem(ProjectManager manager)
        {
            _manager = manager;
        }

        public override string MenuText()
        {
            return "Load TML";
        }

        public override void Select()
        {
            Filer loader = new Filer();

            TMLLoader tmlloader = new TMLLoader();

            loader.RegisterLoader(tmlloader);

            Console.WriteLine("\nPlease enter the file name");

            string filename = Console.ReadLine();

            Filer file = loader.Load(filename + ".tml");
        }
    }

    class LoadTXTMenuItem : MenuItem
    {
        private ProjectManager _manager;

        public LoadTXTMenuItem(ProjectManager manager)
        {
            _manager = manager;
        }

        public override string MenuText()
        {
            return "Load TXT";
        }

        public override void Select()
        {
            Filer loader = new Filer();

            TXTLoader txtloader = new TXTLoader();

            loader.RegisterLoader(txtloader);
            
            Console.WriteLine("\nPlease enter the file name");
            
            string filename = Console.ReadLine();

            Filer file = loader.Load(filename + ".txt");
        }
    }

   

    class NewBuildMenuItem : MenuItem
    {
        private ProjectManager _manager;

        public NewBuildMenuItem(ProjectManager manager)
        {
            _manager = manager;
            
        }

        public override string MenuText()
        {
            return "New Build";
        }

        public override void Select()
        {

            float price;
            
            while (true)
            {
                Console.WriteLine("\nPlease enter the purchase price for the land");
                if (!float.TryParse(Console.ReadLine(), out price) || price < 0)
                {
                    Console.WriteLine("\nError : Invalid Price");
                }
                else
                {
                    break;
                }
               
            }

            try
            {
                Project project = new Project('L', price);
                _manager.AddProject(project);
                Console.WriteLine($"\nProject ID : {project.ProjectID} created\n");
                
            }
            catch { }
            
        }
    }

    class RenovationMenuItem : MenuItem
    {
        private ProjectManager _manager;

        public RenovationMenuItem(ProjectManager manager)
        {
            _manager = manager;

        }

        public override string MenuText()
        {
            return "Renovation";
        }

        public override void Select()
        {

            float price;

            while (true)
            {
                Console.WriteLine("\nPlease enter the price of the renovation");
                if (!float.TryParse(Console.ReadLine(), out price) || price < 0)
                {
                    Console.WriteLine("\nError : Invalid Price");
                }
                else
                {
                    break;
                }


            }

            try
            {
                Project project = new Project('R', price);
                _manager.AddProject(project);
                Console.WriteLine($"\nProject ID : {project.ProjectID} created\n");
            }
            catch { }
        }
    }
}