import { IUserData } from '../../interfaces/IApi/ILoginApi'

const UserDataString = localStorage.getItem('UserData')
export const UserData: IUserData = UserDataString && JSON.parse(UserDataString)
