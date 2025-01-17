﻿using System;
using System.Collections.Generic;
using System.Linq;
using CompanyApi.Dto;
using CompanyApi.Models;

namespace CompanyApi.Services
{
    public class CompanyService : ICompanyService
    {
        public IList<Company> Companies { get; set; }

        public CompanyService()
        {
            Companies = new List<Company>();
        }

        public Company? AddNewCompany(CreateCompanyDto createCompanyDto)
        {
            if (Companies.Any(_ => _.Name.Equals(createCompanyDto.Name, StringComparison.CurrentCultureIgnoreCase)))
            {
                return null;
            }

            var newCompany = new Company(createCompanyDto.Name);
            Companies.Add(newCompany);

            return newCompany;
        }

        public void DeleteAllCompany()
        {
            Companies.Clear();
        }

        public IList<Company> GetCompaniesByPage(int pageSize, int index)
        { 
            return Companies.Skip((pageSize - 1) * pageSize).Take(pageSize).ToList();
        }

        public Company? UpdateCompany(string id, UpdateCompanyDto info)
        {
            var updatingCompany = Companies.FirstOrDefault(_ => _.Id!.Equals(id));
            if (updatingCompany != null)
            {
                updatingCompany.Name = info.Name;
            }

            return updatingCompany;
        }

        public bool DeleteCompany(string id)
        {
            var company = Companies.FirstOrDefault(_ => _.Id!.Equals(id));
            if (company != null)
            {
                Companies.Remove(company);
                return true;
            }

            return false;

        }

        public Employee? AddEmployeeToCompany(string companyId, CreateEmployeeDto createEmployeeDto)
        {
            var company = Companies.FirstOrDefault(_ => _.Id!.Equals(companyId));
            if (company != null)
            {
                var newEmployee = new Employee(createEmployeeDto.Name, createEmployeeDto.Salary);
                company.Employees.Add(newEmployee);
                return newEmployee;
            }

            return null;
        }

        public IList<Employee>? GetAllEmployeesInCompany(string companyId)
        {
            var company = Companies.FirstOrDefault(_ => _.Id!.Equals(companyId));

            return company?.Employees;
        }

        public Employee? UpdateEmployee(string companyId, string employeeId, UpdateEmployeeDto info)
        {
            var company = Companies.FirstOrDefault(_ => _.Id!.Equals(companyId));
            var employee = company?.Employees?.FirstOrDefault(_ => _.Id!.Equals(employeeId));
            if (employee != null)
            {
                employee.Name = info.Name;
                employee.Salary = info.Salary;
            }

            return employee;
        }

        public bool DeleteEmployee(string companyId, string employeeId)
        {
            var company = Companies.FirstOrDefault(_ => _.Id!.Equals(companyId));
            var employee = company?.Employees?.FirstOrDefault(_ => _.Id!.Equals(employeeId));
            if (employee != null)
            {
                company!.Employees!.Remove(employee);
                return true;
            }

            return false;
        }

        public Employee? GetEmployeeById(string companyId, string employeeId)
        {
            var company = Companies.FirstOrDefault(_ => _.Id!.Equals(companyId));
            return company?.Employees?.FirstOrDefault(_ => _.Id!.Equals(employeeId));
        }

        public IList<Company> GetAllCompanies()
        {
            return Companies;
        }

        public Company? GetCompanyById(string id)
        {
            return Companies.FirstOrDefault(_ => _.Id!.Equals(id, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
