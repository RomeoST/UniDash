﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniDash.BLL.Infrastructure;
using UniDash.BLL.Interfaces;
using UniDash.DAL.Infrastructure;
using UniDash.DAL.Repositories;
using UniDash.Model.Models;
using UniDash.Model.Models.StructUniversity;
using Ninject;

namespace UniDash.BLL.Services
{
    public class UStructService : IUStructService
    {
        [Inject] public IUnitOfWork DataBase { get; set; }
        [Inject] public IUStructureRepository<Institute> instituteRepository { get; set; }
        [Inject] public IUStructureRepository<Faculty> facultyRepository { get; set; }
        [Inject] public IUStructureRepository<Department> departmentRepository { get; set; }
        [Inject] public IUStructureRepository<Specialty> specialtyRepository { get; set; }

        /// <summary>
        /// Створення нової структури університету
        /// </summary>
        /// <param name="stucture">Потрібно заповнити 1 вид данних</param>
        /// <param name="type">Тип структури (Факультет, Інститут, Кафедра(Department))</param>
        /// <returns></returns>
        public async Task<OperationDetails> Create(UStructBase stucture, TypeStructUniversity type)
        {
            switch (type)
            {
                case TypeStructUniversity.FACULTY:
                {
                    var faculty = await facultyRepository.GetAsync(p => p.Name == stucture.Faculty.Name);
                    if (faculty != null)
                        return new OperationDetails(false, "Такий факультет вже існує", "");
                    facultyRepository.Add(stucture.Faculty);
                    await SaveStructure();
                    return new OperationDetails(true, "Факультет доданий", "");
                }
                case TypeStructUniversity.INSTITUTE:
                {
                    var institute = await instituteRepository.GetAsync(p => p.Name == stucture.Institute.Name);
                    if (institute != null)
                        return new OperationDetails(false, "Такий інститут вже існує", "");
                    instituteRepository.Add(stucture.Institute);
                    await SaveStructure();
                    return new OperationDetails(true, "Інститут доданий", "");
                 }
                case TypeStructUniversity.DEPARTMENT:
                {
                    var department = await departmentRepository.GetAsync(p => p.Name == stucture.Department.Name);
                    if (department != null)
                        return new OperationDetails(false, "Така кафедра вже існує", "");
                    departmentRepository.Add(stucture.Department);
                    await SaveStructure();
                    return new OperationDetails(true, "Кафедра додана", "");
                }
                default:
                    return new OperationDetails(false, "Тип структури не існує", ""); ;
            }
        }

        /// <summary>
        /// Видалення структури університету
        /// </summary>
        /// <param name="stucture">Потрібно заповнити 1 вид данних</param>
        /// <param name="type">Тип структури (Факультет, Інститут, Кафедра(Department))</param>
        /// <returns></returns>
        public async Task<OperationDetails> Delete(UStructBase stucture, TypeStructUniversity type)
        {
            switch (type)
            {
                case TypeStructUniversity.FACULTY:
                {
                    var faculty = await facultyRepository.GetAsync(p => p.Name == stucture.Faculty.Name);
                    if (faculty == null)
                        return new OperationDetails(false, "Такого факультету не існує", "");
                    facultyRepository.Delete(stucture.Faculty);
                    await SaveStructure();
                    return new OperationDetails(true, $"{stucture.Faculty.Name} - видалений", "");
                }
                case TypeStructUniversity.INSTITUTE:
                {
                    var institute = await instituteRepository.GetAsync(p => p.Name == stucture.Institute.Name);
                    if (institute == null)
                        return new OperationDetails(false, "Такого інституту не існує", "");
                    instituteRepository.Delete(stucture.Institute);
                    await SaveStructure();
                    return new OperationDetails(true, $"{stucture.Institute.Name} - видалений", "");
                }
                case TypeStructUniversity.DEPARTMENT:
                {
                    var department = await departmentRepository.GetAsync(p => p.Name == stucture.Department.Name);
                    if (department == null)
                        return new OperationDetails(false, "Такої кафедри не існує", "");
                    departmentRepository.Delete(stucture.Department);
                    await SaveStructure();
                    return new OperationDetails(true, $"{stucture.Department.Name} - видалений", "");
                }
                default:
                    return new OperationDetails(false, "Тип структури не існує", ""); ;
            }
        }

        public async Task<UStructBase> GetById(int id ,TypeStructUniversity type)
        {
            switch (type)
            {
                case TypeStructUniversity.FACULTY:
                {
                    var faculty = await facultyRepository.GetAsync(p => p.Id == id);
                    return faculty == null ? null : new UStructBase() { Faculty = faculty };
                }
                case TypeStructUniversity.INSTITUTE:
                {
                    var insitute = await instituteRepository.GetAsync(p => p.Id == id);
                    return insitute == null ? null : new UStructBase() { Institute = insitute };
                }
                case TypeStructUniversity.DEPARTMENT:
                {
                    var departament = await departmentRepository.GetAsync(p => p.Id == id);
                    return departament == null ? null : new UStructBase() { Department = departament };
                }
                default:
                    return null;
            }
        }

        public async Task<IEnumerable<UStructBase>> GetAll(TypeStructUniversity type)
        {
            switch (type)
            {
                case TypeStructUniversity.FACULTY:
                {
                    var list = await facultyRepository.GetAllAsync();
                    return list.Select(lq => new UStructBase() { Faculty = lq }).ToList(); 
                }
                case TypeStructUniversity.INSTITUTE:
                {
                    var list = await instituteRepository.GetAllAsync();
                    return list.Select(lq => new UStructBase() { Institute= lq }).ToList();
                }
                case TypeStructUniversity.DEPARTMENT:
                {
                    var list = await departmentRepository.GetAllAsync();
                    return list.Select(lq => new UStructBase() { Department= lq }).ToList();
                }
                default:
                    return null;
            }
        }

        public async Task<IEnumerable<Specialty>> GetSpecialty()
        {
            return await specialtyRepository.GetAllAsync();
        }

        public async Task SaveStructure()
        {
            await DataBase.CommitAsync();
        }
    }
}
