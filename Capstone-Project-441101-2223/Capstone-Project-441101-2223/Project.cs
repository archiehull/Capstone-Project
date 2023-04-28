using System;
namespace Capstone_Project_441101_2223
{
    public class ProjectManager
    {
        public List<Project> Projects;

        private static ProjectManager _Instance;
        
        private ProjectManager()
        {
            Projects = new List<Project>();
        }

        
        public static ProjectManager GetInstance()
        //Singleton of "Projects" list
        {
            if(_Instance == null)
            {
                _Instance = new ProjectManager();
            }
            return _Instance;
        }

        public bool CheckID(int pProjectID)
        //Checks Projects list for matching ID
        {
            return Projects.Any(p => p.ProjectID == pProjectID);
        }

        public void AddProject(Project pProject)
        {
            Projects.Add(pProject);
        }

    }

    public class Project
    {
        public char TypeCode;
        public int ProjectID;
        public float Transaction;


        private static int NextID = 108;

        public int ID { get; private set; }

            

        public Project(int pProjectID, char pTypeCode, float pTransaction)
        {
            if ((pTypeCode == 'L' || pTypeCode == 'R') & (ProjectManager.GetInstance().CheckID(pProjectID) == true))
            //Checks if project has been created under passed ID
            { 
                Console.WriteLine($"\nProject ID : {pProjectID} already exists\n");
                throw new Exception();
            }
            else if ((pTypeCode == 'P' || pTypeCode == 's') & (ProjectManager.GetInstance().CheckID(pProjectID) == false))
            //Checks if project has been created under passed ID
            {
                Console.WriteLine($"\nProject ID : {pProjectID} doesn't exist\n");
                throw new Exception();
            }
            else
            {
                ProjectID = pProjectID;
                TypeCode = pTypeCode;
                Transaction = pTransaction;
            }
            
        }

        public Project(char pTypeCode, float pTransaction)
        //Used when new projects are created - no ID passed through
        {
            while (ProjectManager.GetInstance().CheckID(NextID) == true)
            //check existing IDs - loop until unique ID found
            {
                NextID++;
            }
            ProjectID = NextID;
            TypeCode = pTypeCode;
            Transaction = pTransaction;
        }

        public float TotalPurchase()
        //Returns total "transaction" value of all P, L and R typecodes
        {
            float TotalPurchase = 0;

            for (int i = 0; i < ProjectManager.GetInstance().Projects.Count; i++)
            {
                if (ProjectID == ProjectManager.GetInstance().Projects[i].ProjectID)
                {
                    if (ProjectManager.GetInstance().Projects[i].TypeCode == 'P' || ProjectManager.GetInstance().Projects[i].TypeCode == 'L' || ProjectManager.GetInstance().Projects[i].TypeCode == 'R')
                    {
                        TotalPurchase += ProjectManager.GetInstance().Projects[i].Transaction;

                    }
                }
            }

            return TotalPurchase;
        }

        public float TotalSales()
        //Returns total "transcation" value of all S typecodes
        {
            float TotalSales = 0;

            for (int i = 0; i < ProjectManager.GetInstance().Projects.Count; i++)
            {
                if (ProjectID == ProjectManager.GetInstance().Projects[i].ProjectID)
                {
                    if (ProjectManager.GetInstance().Projects[i].TypeCode == 'S')
                    {
                        TotalSales += ProjectManager.GetInstance().Projects[i].Transaction;
                    }
                }
            }

            return TotalSales;
        }

        public float TaxRefund()
        //Calcuates 20% "VAT tax rebate" on all L typecodes
        {
            float TaxRefund = 0;

            for (int i = 0; i < ProjectManager.GetInstance().Projects.Count; i++)
            {
                if (ProjectID == ProjectManager.GetInstance().Projects[i].ProjectID)
                {
                    if (ProjectManager.GetInstance().Projects[i].TypeCode == 'L')
                    {
                        TaxRefund = (float)((ProjectManager.GetInstance().Projects[i].Transaction) / (1.2));
                    }
                }
            }

            return TaxRefund;

        }

        public float Profit()
        //Use totaling functions to calculate total profit (sales - purchases + tax rebate)
        {
            return TotalSales() - TotalPurchase() + TaxRefund();

        }

        public string Summary()
        //Return total function values
        {
            return $"\nProject ID: {ProjectID}, Purchases: {TotalPurchase()}, Sales: {TotalSales()}, Refund : {TaxRefund()}, Profit : {Profit()}\n";
        }

        public override string ToString()
        //Data output for project instance
        {
            return $"\nID : {ProjectID}, Transaction Type : {TypeCode}, Cost : {Transaction}\n";
        }
    }
}

