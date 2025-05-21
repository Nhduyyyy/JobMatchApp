
using JobMatchApp.DBcontext;
using JobMatchApp.Models;
using JobMatchApp.Services;
using Microsoft.EntityFrameworkCore;

namespace JobMatchApp.Data
{
    public static class JobSeeder
    {
        public static async Task SeedAsync(
            ApplicationDbContext db,
            IEmbeddingService embed)
        {
            if (await db.JobPostings.AnyAsync())
                return;

            var jobs = new[]
{
    new JobPosting {
        Title = "Software Engineer",
        Description = "C#, .NET, SQL, REST APIs",
        RequiredSkills = new[] { "C#", ".NET", "SQL", "REST" }
    },
    new JobPosting {
        Title = "Data Scientist",
        Description = "Python, Machine Learning, Statistics, TensorFlow",
        RequiredSkills = new[] { "Python", "Machine Learning", "Statistics", "TensorFlow" }
    },
    new JobPosting {
        Title = "Front-End Developer",
        Description = "JavaScript, React, HTML5, CSS3, Webpack",
        RequiredSkills = new[] { "JavaScript", "React", "HTML5", "CSS3", "Webpack" }
    },
    new JobPosting {
        Title = "DevOps Engineer",
        Description = "Docker, Kubernetes, AWS, CI/CD, Terraform",
        RequiredSkills = new[] { "Docker", "Kubernetes", "AWS", "CI/CD", "Terraform" }
    },
    new JobPosting {
        Title = "Project Manager",
        Description = "Agile, Scrum, Stakeholder Management, JIRA, Communication",
        RequiredSkills = new[] { "Agile", "Scrum", "Stakeholder Management", "JIRA", "Communication" }
    },
    new JobPosting {
        Title = "QA Engineer",
        Description = "Test Automation, Selenium, C#, NUnit, Performance Testing",
        RequiredSkills = new[] { "Test Automation", "Selenium", "C#", "NUnit", "Performance Testing" }
    },
    new JobPosting {
        Title = "Financial Analyst",
        Description = "Excel, Financial Modeling, Forecasting, SAP, Risk Analysis",
        RequiredSkills = new[] { "Excel", "Financial Modeling", "Forecasting", "SAP", "Risk Analysis" }
    },
    new JobPosting {
        Title = "Marketing Specialist",
        Description = "SEO, SEM, Google Analytics, Content Strategy, Email Marketing",
        RequiredSkills = new[] { "SEO", "SEM", "Google Analytics", "Content Strategy", "Email Marketing" }
    },
    new JobPosting {
        Title = "HR Manager",
        Description = "Recruitment, Employee Relations, Performance Management, HRIS, Labor Law",
        RequiredSkills = new[] { "Recruitment", "Employee Relations", "Performance Management", "HRIS", "Labor Law" }
    },
    new JobPosting {
        Title = "Graphic Designer",
        Description = "Adobe Photoshop, Illustrator, InDesign, Typography, Branding",
        RequiredSkills = new[] { "Photoshop", "Illustrator", "InDesign", "Typography", "Branding" }
    },
    new JobPosting {
        Title = "Network Engineer",
        Description = "Cisco, TCP/IP, Routing, Switching, Network Security",
        RequiredSkills = new[] { "Cisco", "TCP/IP", "Routing", "Switching", "Network Security" }
    },
    new JobPosting {
        Title = "Cybersecurity Analyst",
        Description = "SIEM, Intrusion Detection, Vulnerability Assessment, Firewalls, Incident Response",
        RequiredSkills = new[] { "SIEM", "Intrusion Detection", "Vulnerability Assessment", "Firewalls", "Incident Response" }
    },
    new JobPosting {
        Title = "Business Analyst",
        Description = "Requirement Gathering, UML, SQL, Data Visualization, Stakeholder Management",
        RequiredSkills = new[] { "Requirement Gathering", "UML", "SQL", "Data Visualization", "Stakeholder Management" }
    },
    new JobPosting {
        Title = "Content Writer",
        Description = "Copywriting, Blogging, SEO Writing, Research, CMS (WordPress)",
        RequiredSkills = new[] { "Copywriting", "Blogging", "SEO Writing", "Research", "WordPress" }
    },
    new JobPosting {
        Title = "UX/UI Designer",
        Description = "Wireframing, Prototyping, Figma, User Research, Interaction Design",
        RequiredSkills = new[] { "Wireframing", "Prototyping", "Figma", "User Research", "Interaction Design" }
    },
    new JobPosting {
        Title = "Database Administrator",
        Description = "SQL Server, Backup & Recovery, Performance Tuning, ETL, Indexing",
        RequiredSkills = new[] { "SQL Server", "Backup & Recovery", "Performance Tuning", "ETL", "Indexing" }
    },
    new JobPosting {
        Title = "Mobile App Developer",
        Description = "Kotlin, Swift, React Native, RESTful APIs, UI/UX Mobile",
        RequiredSkills = new[] { "Kotlin", "Swift", "React Native", "RESTful APIs", "UI/UX Mobile" }
    },
    new JobPosting {
        Title = "Sales Representative",
        Description = "Lead Generation, CRM (Salesforce), Negotiation, Presentation, Closing",
        RequiredSkills = new[] { "Lead Generation", "Salesforce", "Negotiation", "Presentation", "Closing" }
    },
    new JobPosting {
        Title = "Supply Chain Manager",
        Description = "Logistics, Inventory Management, SAP, Demand Planning, Supplier Relations",
        RequiredSkills = new[] { "Logistics", "Inventory Management", "SAP", "Demand Planning", "Supplier Relations" }
    },
    new JobPosting {
        Title = "Laboratory Technician",
        Description = "Sample Preparation, PCR, Spectrophotometry, Laboratory Safety, Data Recording",
        RequiredSkills = new[] { "Sample Prep", "PCR", "Spectrophotometry", "Lab Safety", "Data Recording" }
    },
    new JobPosting {
        Title = "Mechanical Engineer",
        Description = "CAD (AutoCAD), SolidWorks, Finite Element Analysis, Mechanical Design, GD&T",
        RequiredSkills = new[] { "AutoCAD", "SolidWorks", "FEA", "Mechanical Design", "GD&T" }
    },
    new JobPosting {
        Title = "Electrical Engineer",
        Description = "Circuit Design, PLC, PCB Layout, MATLAB, Power Systems",
        RequiredSkills = new[] { "Circuit Design", "PLC", "PCB Layout", "MATLAB", "Power Systems" }
    },
    new JobPosting {
        Title = "Chemical Engineer",
        Description = "Process Simulation, Mass Balance, HAZOP, Thermodynamics, Process Control",
        RequiredSkills = new[] { "Process Simulation", "Mass Balance", "HAZOP", "Thermodynamics", "Process Control" }
    },
    new JobPosting {
        Title = "Environmental Scientist",
        Description = "GIS, Environmental Impact Assessment, Soil Testing, Water Quality, Reporting",
        RequiredSkills = new[] { "GIS", "EIA", "Soil Testing", "Water Quality", "Reporting" }
    },
    new JobPosting {
        Title = "Social Media Manager",
        Description = "Content Strategy, Community Management, Analytics, Facebook Ads, Instagram",
        RequiredSkills = new[] { "Content Strategy", "Community Management", "Analytics", "Facebook Ads", "Instagram" }
    },
    new JobPosting {
        Title = "Customer Support Specialist",
        Description = "Zendesk, Troubleshooting, Communication, CRM, SLA Management",
        RequiredSkills = new[] { "Zendesk", "Troubleshooting", "Communication", "CRM", "SLA Management" }
    }
};


            foreach (var j in jobs)
            {
                j.Embedding = (await embed.GetEmbeddingAsync(j.Description)).ToArray();
                db.JobPostings.Add(j);
            }

            await db.SaveChangesAsync();
        }
    }
}