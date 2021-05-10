
using AngularJSAuthentication.Model;
using AngularJSAuthentication.Model.NotMapped;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AngularJSAuthentication.API
{
    public interface iAuthContext
    {

        InvoiceRow AddInvoiceDetail(InvoiceRow e);
        AllInvoice AddInvoice(AllInvoice customer);
        IEnumerable<Customer> AllCustomers { get; }
        Customer AddCustomer(Customer customer);
        IEnumerable<Customer> AllCustomerbyCompanyId(int cmpid);
        Customer PutCustomer(Customer customer);
        bool DeleteCustomer(int id);

        People getPersonIdfromEmail(string email);
        IEnumerable<Company> AllCompanies { get; }
        Company AddCompany(string company);
        Company PutCompany(Company company);
        bool DeleteCompany(int id);
        bool CompanyExists(string companyName);
        Company GetCompanybyCompanyId(int id);

        IEnumerable<ProjectTask> AllProjectTask { get; }
        IEnumerable<ProjectTask> AllProjectTaskbyCompanyId(int cmpid);
        List<ProjectTask> AllProjectTaskByuserId(int userid);
        ProjectTask GetProjectTaskById(int id);
        ProjectTask AddProjectTask(ProjectTask projectTask);
        ProjectTask PutProjectTask(ProjectTask projectTask);
        bool DeleteProjectTask(int id);
        Customer GetClientforProjectId(int projId);
        //IEnumerable<Company> AllCompanies { get; }
        //Company AddCompany(string company);
        //Company PutCompany(Company company);
        //bool DeleteCompany(int id);
        //bool CompanyExists(string companyName);

        IEnumerable<Event> AllEvents(int userid);
        IEnumerable<Event> FilteredEvents(DateTime startDate, DateTime endDate);
        IEnumerable<Event> FilteredEvents(DateTime startDate, DateTime endDate,  int compid);
        IEnumerable<Event> FilteredEvents(DateTime startDate, DateTime endDate,int userid ,int compid);
        Event AddEvent(Event e);
        Event UpdateEvent(Event e);
        bool DeleteEvent(int id);

        Event UpdateEventByViewModel(WeekEventViewModel model, string d,int userid,int compid);
        Event UpdateEventByViewModel(DayEventViewModel model, string d, int userid, int compid);
        IEnumerable<Project> AllProjects { get; }
        IEnumerable<Project> AllProjectsbyCompanyId(int cmpid);
        Project AddProject(Project project);
       
        Project PutProject(Project project);
        bool DeleteProject(int id);


        IEnumerable<TaskType> AllTaskTypes(int compid);
        IEnumerable<TaskType> AllTaskTypesbyCompanyId(int cmpid);
        TaskType AddTaskType(TaskType customer);
        TaskType PutTaskType(TaskType customer);
        bool DeleteTaskType(int id);
        TaskType GetTaskTypeById(int id);


        IEnumerable<People> AllPeoples(int compid);
        People GetPeoplebyCompanyId(int id);
        People AddPeople(People people);
        People PutPeople(People people);
        bool DeletePeople(int id);

       
        List<ClientProject> GetAllClientProject();
        //Customer AddTaskType(TaskType tasktype);
        //Customer UpdateTaskType(TaskType tasktype);
        //bool DeleteTaskType(int id);
        //bool Update(TaskType item);

    }

    public class AuthContext : IdentityDbContext<IdentityUser>, iAuthContext
    {
        public AuthContext()
            : base("AuthContext")
        {

        }

        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<People> Peoples { get; set; }
        //public DbSet<Expense> Expenses { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Customer> Customers { get; set; }
       
        public DbSet<AllInvoice> invoices { get; set; }
        public DbSet<InvoiceRow> InvoiceRows { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        public IEnumerable<Customer> AllCustomers
        {
            get { return Customers.AsEnumerable(); }
        }

        public IEnumerable<TaskType> AllTaskTypes(int compid)
        {
             return TaskTypes.Where(e=> e.CompanyId == compid).AsEnumerable(); 
        }

        public Customer AddCustomer(Customer customer)
        {
            List<Customer> customers = Customers.Where(c => c.Name.Trim().Equals(customer.Name.Trim())).ToList();
            Customer objCustomer = new Customer();
            if (customers.Count == 0)
            {             
                customer.CreatedBy = customer.CreatedBy;
                customer.CreatedDate = DateTime.Now;
                customer.UpdatedDate = DateTime.Now;
                Customers.Add(customer);
                int id = this.SaveChanges();
                //customer.CustomerId = id;
                return customer;
            }
            else
            {
                objCustomer.Exception = "Already";
                return objCustomer;
            }


        }

        public Customer PutCustomer(Customer customer)
        {
            //Customer c = new Customer();
            Customer cust = Customers.Where(x => x.CustomerId == customer.CustomerId).FirstOrDefault();
            if (cust != null)
            {
                cust.UpdatedDate = DateTime.Now;
                cust.Name = customer.Name;

                cust.Description = customer.Description;
                //cust.Address = customer.Address;
                cust.LastModifiedBy = customer.LastModifiedBy;
                Customers.Attach(cust);
                this.Entry(cust).State = EntityState.Modified;
                this.SaveChanges();
                return cust;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteCustomer(int id)
        {
            try
            {
                Customer DL = new Customer();
                DL.CustomerId = id;
                Entry(DL).State = EntityState.Deleted;

                SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }

        }

        public IEnumerable<Project> AllProjects
        {
            get
            {
                if (Projects.AsEnumerable().Count() > 0)
                {
                    return Projects.AsEnumerable();
                }
                else
                {
                 List<Project> project = new List<Project>();
                    return project.AsEnumerable();
                }
                }
        }

        public Project AddProject(Project project)
        {
            List<Project> projects = Projects.Where(c => c.ProjectName.Trim().Equals(project.ProjectName.Trim())).ToList();
            Project objProject = new Project();
            if (projects.Count == 0)
            {
                project.CreatedBy = project.CreatedBy;
                project.CreatedDate = DateTime.Now;                
                project.UpdatedDate = DateTime.Now;
                Projects.Add(project);
                int id = this.SaveChanges();

                //Projects.Add(project);
                ////int ProjectID = this.SaveChanges();
                //project.ProjectID = ProjectID;
                return project;
            }
            else
            {
                //objProject.Exception = "Already";
                return objProject;
            }
        }

        public Project PutProject(Project project)
        {
            Project proj = Projects.Where(x => x.ProjectID == project.ProjectID).FirstOrDefault();
            if (proj != null)
            {
                proj.UpdatedDate = DateTime.Now;
                //proj.CompanyId = objCust.CompanyId;

                proj.ProjectName = project.ProjectName;
                proj.Discription = project.Discription;
                //proj.CompanyId = project.CompanyId;
                proj.Budget = project.Budget;
                proj.StartDate = project.StartDate;
                proj.EndDate = project.EndDate;
                proj.ApproverEmail = project.ApproverEmail;
                proj.ConsultantRate = project.ConsultantRate;
                proj.EmpRate = project.EmpRate;
                proj.CustomerId = project.CustomerId;
                proj.ApproverName = project.ApproverName;

                proj.CreatedBy = project.CreatedBy;
                //proj.CreatedDate = objCust.CreatedDate;
                proj.UpdateBy = project.UpdateBy;

                Projects.Attach(proj);
                this.Entry(proj).State = EntityState.Modified;
                this.SaveChanges();
                return proj;
            }
            else
            {
                return null;
            }
        }       

        public bool DeleteProject(int id)
        {
            try
            {
                Project DL = new Project();
                DL.ProjectID = id;
                Entry(DL).State = EntityState.Deleted;

                SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }

        }

        public TaskType AddTaskType(TaskType tasktype)
        {
            List<TaskType> tasktypes = TaskTypes.Where(c => c.Name.Trim().Equals(tasktype.Name.Trim())).ToList();
            TaskType objTaskType = new TaskType();
            if (tasktypes.Count == 0)
            {
                tasktype.CreateBy = tasktype.CreateBy;
                tasktype.CreatedDate = DateTime.Now;
                tasktype.UpdatedDate = DateTime.Now;
                TaskTypes.Add(tasktype);
                int id = this.SaveChanges();

                return tasktype;
            }
            else
            {
                //  objTaskType.Exception = "Already";
                return null;
            }


        }

        public TaskType PutTaskType(TaskType tasktype)
        {
            //Customer c = new Customer();
            TaskType task = TaskTypes.Where(x => x.Id == tasktype.Id).FirstOrDefault();
            if (task != null)
            {
                task.UpdatedDate = DateTime.Now;
                task.Name = tasktype.Name;

                task.Category = tasktype.Category;

                task.Desc = tasktype.Desc;
                task.UpdatedBy = tasktype.UpdatedBy;
                TaskTypes.Attach(task);
                this.Entry(task).State = EntityState.Modified;
                this.SaveChanges();
                return task;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteTaskType(int id)
        {
            try
            {
                TaskType DL = new TaskType();
                DL.Id = id;                
                Entry(DL).State = EntityState.Deleted;

                SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }

        }

        //public IEnumerable<Event> AllEvents
        //{
        //    get { throw new NotImplementedException(); }
        //}

        public IEnumerable<Event> AllEvents(int userid)
        {
            return Events.Where(e => e.PeopleID == userid).AsEnumerable(); 
        }

        public IEnumerable<ProjectTask> AllProjectTask
        {
            get
            {
                if (ProjectTasks.AsEnumerable().Count() > 0)
                {
                    return ProjectTasks.AsEnumerable();
                }
                else
                {
                    List<ProjectTask> projecttask = new List<ProjectTask>();
                    return projecttask.AsEnumerable();
                }
            }
        }
        public IEnumerable<ProjectTask> AllProjectTaskbyCompanyId(int cmpid)
        {
            return ProjectTasks.Where(c => c.CompanyId == cmpid).AsEnumerable();
        }

        public Event AddEvent(Event e)
        {
            try
            {
                Events.Add(e);
                int id = this.SaveChanges();
            }
            catch (Exception ex)
            { 
            
            }
            return e;
        }

        public Event UpdateEvent(Event e)
        {
            throw new NotImplementedException();
        }

        public bool DeleteEvent(int id)
        {
            Event e = Events.Where(c => c.Id == id).SingleOrDefault();
            if (e != null)
            {
                Entry(e).State = EntityState.Deleted;

                SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public ProjectTask AddProjectTask(ProjectTask projecttask)
        {
            List<ProjectTask> projecttasks = ProjectTasks.Where(c => c.TaskId.Equals(projecttask.TaskId)).ToList();
            Project prj = Projects.Where(x => x.ProjectID == projecttask.ProjectID).Select(x=>x).FirstOrDefault();
            People ppl = Peoples.Where(x => x.Deleted == false).Where(x => x.PeopleID == projecttask.PeopleID).Select(x => x).FirstOrDefault();
            Customer cust = Customers.Where(x => x.CustomerId == projecttask.CustomerId).Select(x => x).FirstOrDefault();
            ProjectTask objProjectTask = new ProjectTask();           

            if (projecttasks.Count == 0)
            {
                projecttask.CreatedBy = projecttask.CreatedBy;
                projecttask.CreatedDate = DateTime.Now;
                projecttask.UpdatedDate = DateTime.Now;
                //projecttask.ProjectName = projecttask.ProjectName;
                projecttask.ProjectName = prj.ProjectName;
                projecttask.CustomerName = cust.Name;
                projecttask.PeopleID = ppl.PeopleID;

                ProjectTasks.Add(projecttask);
                int id = this.SaveChanges();               
                return projecttask;
            }
            else
            {
                //objProject.Exception = "Already";
                return objProjectTask;
            }
            //this.ProjectTasks.Add(projecttask);
            //this.SaveChanges();
            //return projecttask;
        }

        public ProjectTask PutProjectTask(ProjectTask objCust)
        {
            ProjectTask proj = ProjectTasks.Where(x => x.TaskId == objCust.TaskId).FirstOrDefault();
            Project prj = Projects.Where(x => x.ProjectID == objCust.ProjectID).Select(x => x).FirstOrDefault();
            Customer cust = Customers.Where(x => x.CustomerId == objCust.CustomerId).Select(x => x).FirstOrDefault();
            if (proj != null)
            {
                proj.UpdatedDate = DateTime.Now;
                proj.TaskTypeId = objCust.TaskTypeId;
                proj.AllocatedHours = objCust.AllocatedHours;
                proj.Priority = objCust.Priority;
                proj.Name = objCust.Name;
                proj.StartDate = objCust.StartDate;
                proj.EndDate = objCust.EndDate;
                proj.CustomerId = objCust.CustomerId;
                proj.ProjectID = objCust.ProjectID;
                proj.Assignee = objCust.Assignee;

                proj.ProjectName = prj.ProjectName;
                proj.CustomerName = cust.Name;

                proj.Discription = objCust.Discription;
                proj.CreatedBy = objCust.CreatedBy;
                proj.UpdatedDate = DateTime.Now;
                proj.UpdateBy = objCust.UpdateBy;

                ProjectTasks.Attach(proj);
                this.Entry(proj).State = EntityState.Modified;
                this.SaveChanges();
                return objCust;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteProjectTask(int id)
        {
            try
            {
                ProjectTask DL = new ProjectTask();
                DL.TaskId = id;
                Entry(DL).State = EntityState.Deleted;

                SaveChanges();
                return true;
            }
            catch
            {

                return false;
            }
        }

        //This method is used for get all client projects
        public List<ClientProject> GetAllClientProject()
        {
            List<ClientProject> ClientProjects = new List<ClientProject>();


            //var q = (from pd in this.Projects
            //         join 
            //         od in this.Clients on 
            //         pd.ClientID equals od.Id
            //         orderby pd.ProjectName
            //         select new ClientProject
            //         {
            //             Name = od.Name + "-" + pd.ProjectName,
            //             Id = pd.ProjectID

            //         }).ToList();
            try
            {
                //IEnumerable<Project> prj= Projects
                //                .Include("Client") 
                //               .Select(x => x).ToList();


                foreach (var a in Projects)
                {
                    ClientProject cli = new ClientProject();
                    cli.Id =  a.CustomerId;
                    cli.Name = a.ProjectName;
                    ClientProjects.Add(cli);
                
                }
                return ClientProjects;
            }
            catch (Exception ex)
            {
                return null; 
            }
        }

        public IEnumerable<Event> FilteredEvents(DateTime startDate, DateTime endDate)
        {
            List<Event> events = Events.Where(e => e.EventDate <= endDate && e.EventDate >= startDate).ToList();
            return events.AsEnumerable();
        }

        public IEnumerable<Company> AllCompanies
        {
            get { return Companies.AsEnumerable(); }
        }

        public Company AddCompany(string companyname)
        {
            List<Company> cmp = Companies.Where(c => c.Name.Trim().Equals(companyname.Trim())).ToList();
            Company objCompany = new Company();
            if (cmp.Count == 0)
            {
                objCompany.Name = companyname;
                objCompany.CreatedBy = "System";
                objCompany.CreatedDate = DateTime.Now;
                objCompany.UpdatedDate = DateTime.Now;
                Companies.Add(objCompany);
                int id = this.SaveChanges();
                //Company.CompanyId = id;
                return objCompany;
            }
            else
            {

                return cmp[0];
            }
        }

        public Company PutCompany(Company company)
        {
            Company proj = Companies.Where(x => x.Id == company.Id).FirstOrDefault();
            if (proj != null)
            {
                proj.UpdatedDate = DateTime.Now;
                //proj.Name = cmp.Name;
                proj.AlertDay = company.AlertDay;
                proj.AlertTime = company.AlertTime;
                proj.FreezeDay = company.FreezeDay;
                proj.TFSUrl = company.TFSUrl;
                proj.TFSUserId = company.TFSUserId;
                proj.TFSPassword = company.TFSPassword;
                proj.LogoUrl = company.LogoUrl;

                proj.Address = company.Address;
                proj.CompanyName = company.CompanyName;
                proj.contactinfo = company.contactinfo;
                proj.currency = company.currency;
                proj.dateformat = company.dateformat;
                proj.fiscalyear = company.fiscalyear;
                proj.startweek = company.startweek;
                proj.timezone = company.timezone;
                proj.Webaddress = company.Webaddress;

              //  proj.UpdateBy = company.UpdateBy;

            Companies.Attach(proj);
                this.Entry(proj).State = EntityState.Modified;
                this.SaveChanges();
                return proj;
            }
            else
            {
                return null;
            }
        }

        public bool DeleteCompany(int id)
        {
            throw new NotImplementedException();
        }

        public bool CompanyExists(string companyName)
        {
            List<Company> cmp = Companies.Where(c => c.Name.Trim().Equals(companyName.Trim())).ToList();
            Company objCompany = new Company();
            if (cmp.Count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public IEnumerable<Customer> AllCustomerbyCompanyId(int cmpid)
        {
            return Customers.Where(c => c.CompanyId == cmpid).AsEnumerable();
        }

        public IEnumerable<Project> AllProjectsbyCompanyId(int cmpid)
        {
            return Projects.Where(c => c.CompanyId == cmpid).AsEnumerable();
        }

        public IEnumerable<TaskType> AllTaskTypesbyCompanyId(int cmpid)
        {
            return TaskTypes.Where(c => c.CompanyId == cmpid).AsEnumerable();
        }

        public IEnumerable<People> AllPeoples(int compid)
        {
          
                if (Peoples.AsEnumerable().Count() > 0)
                {
                List<People> person = new List<People>();
                person = Peoples.Where(x => x.Deleted == false).Where(e => e.CompanyID == compid).ToList();
                return person.AsEnumerable();
                }
                else

                {
                    List<People> people = new List<People>();
                    return people.AsEnumerable();
                }
            
        }

        public People AddPeople(People people)
        {
            List<People> peoples = Peoples.Where(x => x.Deleted == false).Where(c => c.Email.Trim().Equals(people.Email.Trim())).ToList();
            People objPeople = new People();
            if (peoples.Count == 0)
            {
                people.CreatedBy = people.CreatedBy;
                people.CreatedDate = DateTime.Now;
                people.UpdatedDate = DateTime.Now;
                Peoples.Add(people);
                int id = this.SaveChanges();

                //Projects.Add(project);
                ////int ProjectID = this.SaveChanges();
                //project.ProjectID = ProjectID;
                return people;
            }
            else
            {
                //objProject.Exception = "Already";
                return objPeople;
            }
        }

        public People PutPeople(People objCust)
        {
            People proj = Peoples.Where(x => x.Deleted == false).Where(x => x.PeopleID == objCust.PeopleID).FirstOrDefault();
            if (proj != null)
            {
                proj.UpdatedDate = DateTime.Now;
                //proj.Client = objCust.Client;
                proj.PeopleFirstName = objCust.PeopleFirstName;
                proj.PeopleLastName = objCust.PeopleLastName;
                proj.Email = objCust.Email;                
                proj.Department = objCust.Department;
                proj.BillableRate = objCust.BillableRate;
                proj.CostRate = objCust.CostRate;
                proj.Permissions = objCust.Permissions;
                proj.Type = objCust.Type;
                proj.ImageUrl = objCust.ImageUrl;

                proj.CreatedBy = objCust.CreatedBy;
                proj.CreatedDate = objCust.CreatedDate;
                proj.UpdateBy = objCust.UpdateBy;
                proj.EmailConfirmed = objCust.EmailConfirmed;
                Peoples.Attach(proj);
                this.Entry(proj).State = EntityState.Modified;
                this.SaveChanges();
                return objCust;
            }
            else
            {
                return objCust;
            }
        }

        public bool DeletePeople(int id)
        {
            try
            {
                People DL = new People();
                DL.PeopleID = id;
                Entry(DL).State = EntityState.Deleted;

                SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Company GetCompanybyCompanyId(int id)
        {
            Company  p = this.Companies.Where(c => c.Id == id).SingleOrDefault();
            if (p != null)
            {
                
            }
            else
            {

                p = new Company();

            }
            return p;
        }

        public Event UpdateEventByViewModel(WeekEventViewModel model, string d,int userid,int compid)
        {
            Event e = new Event();
         switch(d)
         {
             case "d1" :
                 {
                   e = this.Events.Where(c => c.Id == model.d1EventId).SingleOrDefault();
                  e.Hours = model.d1;
                  e.UpdatedDate = DateTime.Now;
                  e.TaskType = model.tasktypeid;
                        e.TaskId = model.taskid;
                  e.ProjectId = model.projectid;
                        Project proj =this.Projects.Where(p => p.ProjectID == model.projectid).SingleOrDefault();
                        e.ClientId = proj.CustomerId;
                  e.ProjectName = model.projectname;
                     break;
                 }
           
             case "d2":
                 {
                     e = this.Events.Where(c => c.Id == model.d2EventId).SingleOrDefault();
                     e.Hours = model.d2;
                     e.UpdatedDate = DateTime.Now;
                     e.TaskType = model.tasktypeid;
                     e.ProjectId = model.projectid;
                        e.TaskId = model.taskid;
                        e.ProjectName = model.projectname;
                     break;
                 }
             case "d3":
                 {
                     e = this.Events.Where(c => c.Id == model.d3EventId).SingleOrDefault();
                     e.Hours = model.d3;
                     e.UpdatedDate = DateTime.Now;
                     e.TaskType = model.tasktypeid;
                     e.ProjectId = model.projectid;
                        e.TaskId = model.taskid;
                        e.ProjectName = model.projectname;
                     break;
                 }
             case "d4":
                 {
                     e = this.Events.Where(c => c.Id == model.d4EventId).SingleOrDefault();
                     e.Hours = model.d4;
                     e.UpdatedDate = DateTime.Now;
                     e.TaskType = model.tasktypeid;
                     e.ProjectId = model.projectid;
                        e.TaskId = model.taskid;
                        e.ProjectName = model.projectname;
                     break;
                 }
             case "d5":
                 {
                     e = this.Events.Where(c => c.Id == model.d5EventId).SingleOrDefault();
                     e.Hours = model.d5;
                     e.UpdatedDate = DateTime.Now;
                     e.TaskType = model.tasktypeid;
                     e.ProjectId = model.projectid;
                        e.TaskId = model.taskid;
                        e.ProjectName = model.projectname;
                     break;
                 }
             case "d6":
                 {
                     e = this.Events.Where(c => c.Id == model.d6EventId).SingleOrDefault();
                     e.Hours = model.d6;
                     e.UpdatedDate = DateTime.Now;
                     e.TaskType = model.tasktypeid;
                     e.ProjectId = model.projectid;
                        e.TaskId = model.taskid;
                        e.ProjectName = model.projectname;
                     break;
                 }
             case "d7":
                 {
                     e = this.Events.Where(c => c.Id == model.d7EventId).SingleOrDefault();
                     e.Hours = model.d7;
                     e.UpdatedDate = DateTime.Now;
                     e.TaskType = model.tasktypeid;
                     e.ProjectId = model.projectid;
                        e.TaskId = model.taskid;
                        e.ProjectName = model.projectname;
                     break;
                 }
             default :
                     {
                     break;
                     }
         }
         Events.Attach(e);
         this.Entry(e).State = EntityState.Modified;
         this.SaveChanges();
         return e;
       
           
        }

        public IEnumerable<Event> FilteredEvents(DateTime startDate, DateTime endDate,  int compid)
        {
            List<Event> events = Events.Where(e => e.EventDate <= endDate && e.EventDate >= startDate && e.CompanyId == compid).ToList();
            return events.AsEnumerable();
          
        }

        public IEnumerable<Event> FilteredEvents( DateTime startDate, DateTime endDate, int userid,int compid)
        {
            List<Event> events = Events.Where(e => e.EventDate <= endDate && e.EventDate >= startDate && e.PeopleID == userid).ToList();
            return events.AsEnumerable();
           
        }


        public People getPersonIdfromEmail(string email)
        {
            People ps = new People();
            ps = Peoples.Where(x => x.Deleted == false).Where(p => p.Email.Trim().Equals(email.Trim())).SingleOrDefault();
            int id = 0;
          if(ps!= null)
          {
              id = ps.PeopleID;
            }
            return ps;
        }
        //public int updatePersonFromEmail(string email)
        //{
        //    People ps = Peoples.Where(p => p.Email.Trim().Equals(email.Trim())).SingleOrDefault();
        //    int id = 0;
        //    if (ps != null)
        //    {
        //        ps.C
        //        id = ps.PeopleID;
        //    }
        //    return id;
        //}
        
        public Customer GetClientforProjectId(int projId)
{
    Project project = Projects.Where(p => p.ProjectID == projId).SingleOrDefault();
    Customer client = Customers.Where(p => p.CustomerId ==  project.CustomerId).SingleOrDefault(); ;
    return client;
}
        
        public TaskType GetTaskTypeById(int id)
{
    TaskType tt = TaskTypes.Where(p => p.Id == id).SingleOrDefault();
    return tt;
}


        public Event UpdateEventByViewModel(DayEventViewModel model, string d, int userid, int compid)
{
    Event e = new Event();
    switch (d)
    {
        case "d1":
            {
                e = this.Events.Where(c => c.Id == model.d1EventId).SingleOrDefault();
                e.Hours = model.d1;
                e.UpdatedDate = DateTime.Now;
                e.TaskType = model.tasktypeid;
                e.ProjectId = model.projectid;
                e.ProjectName = model.projectname;
                break;
            }

       
        default:
            {
                break;
            }
    }
    Events.Attach(e);
    this.Entry(e).State = EntityState.Modified;
    this.SaveChanges();
    return e;


}


        public People GetPeoplebyCompanyId(int id)
{
    People p = this.Peoples.Where(x => x.Deleted == false).Where(c => c.PeopleID == id).SingleOrDefault();
    if (p != null)
    {

    }
    else
    {

        p = new People();

    }
    return p;
}

        public List<ProjectTask> AllProjectTaskByuserId(int userid)
        {
            List<ProjectTask> p = this.ProjectTasks.Where(c => c.PeopleID == userid && c.Completed == false).ToList();
            if (p != null)
            {

            }
            else
            {

                p = new List<ProjectTask>();

            }
            return p;
        }

        public ProjectTask GetProjectTaskById(int id)
        {
            ProjectTask p = this.ProjectTasks.Where(c => c.TaskId == id && c.Completed == false).SingleOrDefault();
            return p;
        }
        public AllInvoice AddInvoice(AllInvoice invoice)
        {
            List<AllInvoice> inv = invoices.Where(c => c.id.Equals(invoice.id)).ToList();
            Customer cust = Customers.Where(x => x.CustomerId == invoice.CustomerId).Select(x => x).FirstOrDefault();
            AllInvoice obj = new AllInvoice();
            if (inv.Count == 0)
            {

                //  invoice.Issuedate = DateTime.Now;
                invoice.lastdate = DateTime.Now;

                try
                {
                    invoice.Customer = cust.Name;
                    invoices.Add(invoice);
                    int id = this.SaveChanges();
                    //invoice.InvoiceID = id;
                    return invoice;
                }
                catch
                {

                }
            }
            else
            {
                //  objTaskType.Exception = "Already";
                return null;
            }

            return obj;
        }

        public InvoiceRow AddInvoiceDetail(InvoiceRow e)
        {
            try
            {
                InvoiceRows.Add(e);
                int id = this.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return e;
        }
     }
}