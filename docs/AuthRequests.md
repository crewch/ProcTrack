## Сервис авторизации

### Dtos

- AuthDto {Email, Password} - Входные данные для авторизации
- UserDto {Id, Email, LongName, ShortName, Roles} - данные User
- GroupDto {Id, Title, Description, Boss (UserDto)} - данные группы 
- UserHoldTypeDto {Id (User), HoldType (Process / Stage)} - Входные данные для получения списка Holds для определенного User-а и типа (процесс или этап)
- HoldDto {DestId (Id Process или Id Stage), Type, Rights, Users, Groups} - данные Hold
- HolderDto {Id (User или Group), Type ("User" или "Group")} - данные Holder (это мб User или Group)
- CreateHoldRequestDto {DestId (Id Process или Id Stage), Type (Process или Stage), HolderType (User или Group), HolderTypeId (Id User / Id Group)} - Входные данные для создания Hold
- CreateHoldResponceDto {Hold (HoldDto), Holder }

### Сервисы

- Login Service
- Hold Service
- User Service

### Эндпоинты

#### Авторизация
- POST /api/auth/login              Авторизация
#### Пользователи
- GET /api/auth/user/{id}           Получение пользователя по его Id               
#### Холды
- POST /api/auth/holds/get          Получение всех Hold
- POST /api/auth/holds/create       Создание Hold
- GET /api/auth/holds/{id}          Получение Hold по Id

### Запросы

- Авторизация (Authorize) + (добавить LDAP)

Вход: AuthDto 
Выход: UserDto 

- Получение Holds и прав (getHolds)

Вход: UserHoldtypeDto
Выход: {List<HoldDto> (Без User и Group)}

- создание Hold

Вход: 
Выход: CreateHoldResponceDto

- получение Hold-а со всеми Users и Groups 

Вход: HoldId
Выход: HoldDto

- получение пользователя по его id
Вход: id
Выход: UserDto


## Сервис процессов



### Dtos
- CommentDto {id (int), text (string), user (UserDto), date_created (datetime)}
- TaskDto {id (int), title (string), date_start (datetime), date_end_verification (datetime), date_approval (datetime), time_expected (interval), user (UserDto) /* это пользовтель взявший задачу в работу */, comments (List <CommentDto>)}
- StageDto {id (int), title (string), date_create (datetime), date_signed (datetime), status (string), user (UserDto) /* человек утвердивший этап */, hold (HoldDto)}
- ProcessDto {id (int), priority (string), type (string), date_create (datetime), date_approved (datetime), time_expected (interval), hold (HoldDto)}
- LinkDto {edges (List<Pair<int, int>>), dependences (List<Pair<int, int>>), staus (string) /* это мы вычисляем на беке */}
- CreateProcessDto {template_id (int), group_id (int), process (ProcessDto /* тут не все поля, поэтому в ProcessDto нужно чтобы все поля могли быть null или не присылаться */)}

### Сервисы
- Process Service
- Stage Service
- Task Service
- Property Service

### Эндпоинты
#### Property
- GET api/track/process/templates {List<ProcessDto>} - получить доступные шаблоны процессов
- GET api/track/process/priority {List<string>} - получить доступные приоритеты
<!-- - GET api/track/process/type -->

#### Process
- GET api/track/process/get {user_id, List<ProcessDto>} - получить список процессов доступных на чтение или изменение
- POST api/track/process/create {CreateProcessDto + user_id, ProcessDto} - добавить новый процесс
- PUT api/track/process/{id}/update {user_id + ProcessDto, ProcessDto} - изменить существующий процесс
- GET api/track/process/{id} {id, ProcessDto} - получить процесс по id
- GET api/track/process/{id}/stage {id, List<StageDto>} - получить список этапов данного процесса
- GET api/track/process/{id}/link {id, LinkDto} - получить списки из связей и зависимостей этапов (edge и depence)
- GET api/track/process/{id}/start {user_id + id, ProcessDto} - запустить процесс в работу
- GET api/track/process/{id}/stop {user_id + id, Process Dto} - остановить процесс
#### Stage
- GET api/track/stage/get {user_id, List<StageDto>} - получить список этапов доступных на чтение или изменение

- GET api/track/stage/{id}/assign {user_id + id, StageDto} - утвердить этап
- PUT api/track/stage/{id}/update {user_id + StageDto, StageDto} - изменить этап
- GET api/track/stage/{id} {id, StageDto} - получить этап по id
- GET api/track/stage/{id}/task {id, List<TaskDto>} - получить список заданий данного этапа
#### Task
- GET api/track/task/{id}/assign {user_id, TaskDto} - согласовать задание
- GET api/track/task/{id} {id, TaskDto} - получить задание по id задания
- GET api/track/task/{id}/start {user_id, TaskDto} - начать выполнение задания
- GET api/track/task/{id}/stop {user_id, TaskDto} - отменить выполнение задания
- GET api/track/task/{id}/comment/get {id, List<Comment>} - получить список комментариев к заданию
- POST api/track/task/{id}/comment/create {id + CommentDto, CommentDto} - добавить новый комментарий

### Запросы

- Создать новый процесс
{CreateProcessDto + user_id, ProcessDto}
- Получить доступные пользователю процессы
{user_id, List<ProcessDto>}
- изменить процесс
{user_id + ProcessDto, ProcessDto}
- получить процесс по id 
{id, ProcessDto}
- получить список этапов процесса по id 
{id, List<StageDto>}
- получить зависимости (edge и depence) для процесса по id
{id, LinkDto}
- запустить процесс в работу
{user_id + id, ProcessDto}
- остановить процесс
{user_id + id, Process Dto}
- получить шаблоны процессов
{List<ProcessDto>}
- получить доступные приоритеты
{List<string>}
