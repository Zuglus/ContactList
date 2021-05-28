# ContactList
Список контактов. Одна из частей электронного органайзера

### Позволяет:
― Добавлять, удалять, редактировать записи;  
― Поиск контакта по любому полю.

#### Список полей:
  - Фамилия;
  - Имя;
  - Отчество;
  - Датарождения;
  - Организация;
  - Должность;
  - Контактная информация:
    - Телефон;
    - Email;
    - Skype;
    - Другое.
    
>Поля контактной информации могут быть использованы несколько раз для
одного контакта (один контакт может иметь несколько телефонных номеров).

## Технологический стек
― Microsoft SQL Server 2019 Developer Edition;  
― Microsoft .Net Framework 5.6:
  - Entity Framework;
  - ASP.Net MVC.
  
― Публичная система контроля версий GitHub.

## Подключение базы данных
В Microsoft Visual Studio Community 2019 через диспетчер пакетов NuGet установить:
- Microsoft.EntityFrameworkCore.SqlServer;
- Microsoft.EntityFrameworkCore.Tools;
- Microsoft.VisualStudio.Web.CodeGeneration.Design

В консоли диспетчера пакетов набрать (Средства -> Диспетер пакетов NuGet -> Консоль диспетчера пакетов)
``` Nuget
Add-Migration InitialCreate
Update-Database
```
