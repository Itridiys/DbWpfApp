using System.Collections.Generic;
using System.Linq;
using DbWpfApp.Models.Data;

namespace DbWpfApp.Models
{
    public static class DataWorker
    {
        /// <summary>
        /// Получить все департаменты
        /// </summary>
        /// <returns></returns>
        public static List<Department> GetAllDepartments()
        {
            using (AplicationContext db = new AplicationContext())
            {
                var result = db.Departments.ToList();
                return result;
            }
        }

        /// <summary>
        /// Получить все позиции
        /// </summary>
        /// <returns></returns>
        public static List<Position> GetAllPositions()
        {
            using (AplicationContext db = new AplicationContext())
            {
                var result = db.Positions.ToList();
                return result;
            }
        }

        /// <summary>
        /// Получить всех Юзеров
        /// </summary>
        /// <returns></returns>
        public static List<User> GetAllUsers()
        {
            using (AplicationContext db = new AplicationContext())
            {
                var result = db.Users.ToList();
                return result;
            }
        }

        /// <summary>
        /// Создать Департамент
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CreateDepartment(string name)
        {
            string result = "Уже существует";
            using (AplicationContext db = new AplicationContext() )
            {
                //провека существует ли отдел
                bool checkIsExist = db.Departments.Any(element => element.Name == name);
                if (!checkIsExist)
                {
                    Department newDepartment = new Department {Name = name};
                    db.Departments.Add(newDepartment);
                    db.SaveChanges();
                    result = "Cделано!";
                }

                return result;
            }
        }

        /// <summary>
        /// Создать позицию
        /// </summary>
        /// <param name="name"></param>
        /// <param name="salary"></param>
        /// <param name="maxNumber"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        public static string CreatePasition(string name, decimal salary, int maxNumber, Department department)
        {
            string result = "Уже существует";
            using (AplicationContext db = new AplicationContext())
            {
                //провека существует ли позиция
                bool checkIsExist = db.Positions.Any(element => element.Name == name & element.Salary == salary);
                if (!checkIsExist)
                {
                    Position newPasition = new Position
                    {
                        Name = name, 
                        Salary = salary,
                        MaxNumber = maxNumber,
                        DepartmentId = department.Id
                    };
                    db.Positions.Add(newPasition);
                    db.SaveChanges();
                    result = "Cделано!";
                }

                return result;
            }
        }

        /// <summary>
        /// Создать сотрудника
        /// </summary>
        /// <param name="name"></param>
        /// <param name="salary"></param>
        /// <param name="maxNumber"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        public static string CreateUser(string name, string surName, string phone, Position position)
        {
            string result = "Уже существует";
            using (AplicationContext db = new AplicationContext())
            {
                //провека существует ли позиция
                bool checkIsExist = db.Users.Any(element => element.Name == name && element.SurName == surName && element.Position == position);
                if (!checkIsExist)
                {
                    User newUser = new User
                    {
                        Name = name,
                        SurName = surName,
                        Phone = phone,
                        PositionId = position.Id
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    result = "Cделано!";
                }

                return result;
            }
        }

        /// <summary>
        /// Удаление департамента
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public static string DeleteDepartment (Department department)
        {
            string result = "Такого департамента не существует";
            using (AplicationContext db = new AplicationContext() )
            {
                db.Departments.Remove(department);
                db.SaveChanges();
                result = $"Сделано! Отдел {department.Name} удален";
            }

            return result;
        }

        /// <summary>
        /// Удаление позиции
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public static string DeletePosition(Position position)
        {
            string result = "Такой позиции не существует";
            using (AplicationContext db = new AplicationContext())
            {
                db.Positions.Remove(position);
                db.SaveChanges();
                result = $"Сделано! Позиция {position.Name} удалена";
            }

            return result;
        }

        /// <summary>
        /// Удаление сотрудника
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static string DeleteUser(User user)
        {
            string result = "Такого пользователя не существует";
            using (AplicationContext db = new AplicationContext())
            {
                db.Users.Remove(user);
                db.SaveChanges();
                result = $"Сделано! Сотрудник {user.Name} удален";
            }

            return result;
        }

        /// <summary>
        /// Изменить имя департамента
        /// </summary>
        /// <param name="oldDepartment"></param>
        /// <param name="newName"></param>
        /// <returns></returns>
        public static string EditDepartment(Department oldDepartment, string newName)
        {
            string result = "Такого департамента не существует";

            using (AplicationContext db = new AplicationContext())
            {
                Department department = db.Departments.FirstOrDefault(d => d.Id == oldDepartment.Id);
                department.Name = newName;
                db.SaveChanges();
                result = $"Департамент {department.Name} переименован в {newName}";
            }

            return result;
        }

        /// <summary>
        /// Изменения позиции
        /// </summary>
        /// <param name="oldPosition"></param>
        /// <param name="newName"></param>
        /// <param name="newMaxNumber"></param>
        /// <param name="newSalary"></param>
        /// <param name="newDepartment"></param>
        /// <returns></returns>
        public static string EditPosition(Position oldPosition, string newName, int newMaxNumber, decimal newSalary, Department newDepartment)
        {
            string result = "Такой позиции не существует";

            using (AplicationContext db = new AplicationContext())
            {
                Position position = db.Positions.FirstOrDefault(d => d.Id == oldPosition.Id);
                if (position != null)
                {
                    position.Name = newName;
                    position.Salary = newSalary;
                    position.MaxNumber = newMaxNumber;
                    position.DepartmentId = newDepartment.Id;
                    db.SaveChanges();
                    result = $"Позиция {position.Name} изменина";
                }
            }

            return result;
        }

        public static string EditUser(User oldUser, string newName, string newSurName, string newPhone,
            Position position)
        {
            string result = "Такого пользователя не существует";
            using (AplicationContext db = new AplicationContext())
            {
                User user = db.Users.FirstOrDefault(p => p.ID == oldUser.ID);
                if (user != null)
                {
                    user.Name = newName;
                    user.SurName = newSurName;
                    user.Phone = newPhone;
                    user.PositionId = position.Id;
                    db.SaveChanges();
                    result = $"Пользователь {user.Name} изменён";
                }
            }
            return result;
        }

        /// <summary>
        /// Получение позиции по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Position GetPositionById(int id)
        {
            using (AplicationContext db = new AplicationContext())
            {
                Position pos = db.Positions.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }

        /// <summary>
        /// Получение Департамента по ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Department GetdDepartmentById(int id)
        {
            using (AplicationContext db = new AplicationContext())
            {
                Department pos = db.Departments.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }

        /// <summary>
        /// Get Users by ID position
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<User> GetdUsersByPositionId(int id)
        {
            using (AplicationContext db = new AplicationContext())
            {
                List<User> users = (from user in GetAllUsers() where user.PositionId == id select user).ToList();
                return users;
            }
        }

        public static List<Position> GetdPositionsBydepartmentId(int id)
        {
            using (AplicationContext db = new AplicationContext())
            {
                List<Position> positions = (from position in GetAllPositions() where position.DepartmentId == id select position).ToList();
                return positions;
            }
        }
    }
}
