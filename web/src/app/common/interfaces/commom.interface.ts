export interface IDropDownItem {
    display: string;
    value: string;
    isActive: boolean;
    children?: IDropDownItem[];
}

export interface IBanks extends IDropDownItem {
    siteID: string;
}

export interface IAccountAccountInfoShort
{
    firstName: string;
    lastName: string;
    labelAccountName: string;
    role: string;
}

export interface IUser {
    workdayNumber: number;
    firstName: string;
    lastName: string;
    fullName: string;
    username: string;
    email: string;
    isActive: boolean;
}