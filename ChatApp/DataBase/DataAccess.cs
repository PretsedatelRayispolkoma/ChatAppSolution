using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using CringeChat.DataBase;
using System.Collections;

namespace CringeChat.DataBase
{
    public static class DataAccess
    {
        public delegate void RefreshListDelegate();
        public static event RefreshListDelegate RefreshListEvent;

        public static List<Employee> GetEmployees() => ChatAppDBEntities.GetContext().Employee.ToList(); 
        public static List<Department> GetDepartments() => ChatAppDBEntities.GetContext().Department.ToList(); 

        public static List<Chatroom> GetChatrooms() => ChatAppDBEntities.GetContext().Chatroom.ToList();

        public static Employee GetEmployee(string username, string password)
        {
            return ChatAppDBEntities.GetContext().Employee.FirstOrDefault(x => x.Username == username && x.Password == password);
        }

        internal static void SaveChatroom(Chatroom chatroom)
        {
            if (chatroom.Id == 0)
                ChatAppDBEntities.GetContext().Chatroom.Add(chatroom);

            ChatAppDBEntities.GetContext().SaveChanges();
            RefreshListEvent?.Invoke();
        }

        public static List<ChatMessage> GetChatMessages() => ChatAppDBEntities.GetContext().ChatMessage.ToList();

        public static void LeaveChat(Employee employee, Chatroom chatroom)
        {
            var employeeChat = ChatAppDBEntities.GetContext().EmployeeChat
                .FirstOrDefault(x => x.EmployeeId == employee.Id && x.ChatId == chatroom.Id);
            if (employeeChat != null)
                ChatAppDBEntities.GetContext().EmployeeChat.Remove(employeeChat);

            ChatAppDBEntities.GetContext().SaveChanges();
            RefreshListEvent?.Invoke();
        }

        public static List<ChatMessage> GetChatMessages(Chatroom chatroom)
        {
            return GetChatMessages().FindAll(x => x.Chatroom == chatroom);
        }

        public static void SaveChatMessage(ChatMessage message)
        {
            if (message.Id == 0)
                ChatAppDBEntities.GetContext().ChatMessage.Add(message);

            ChatAppDBEntities.GetContext().SaveChanges();
            RefreshListEvent?.Invoke();
        }


        static string ComputeStringToSha256Hash(string plainText)
        {
            // Create a SHA256 hash from string   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Computing Hash - returns here byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(plainText));

                // now convert byte array to a string   
                StringBuilder stringbuilder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    stringbuilder.Append(bytes[i].ToString("x2"));
                }
                return stringbuilder.ToString();
            }
        }
    }
}
