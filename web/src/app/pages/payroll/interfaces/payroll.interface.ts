import { IBanks, IDropDownItem } from "src/app/common/interfaces/commom.interface";

export interface IEmployeeInit {
    jobs : IDropDownItem[];
    sites: IDropDownItem[];
    banks: IBanks[];
    sups: IDropDownItem[];
}

export interface IEmployeeShort {
    workdayNumber: number;
    identificationCard: string;
    firstName: string;
    lastName: string;
    fullName: string;
    isActive: boolean;
}

export interface IEmployeeLong extends IEmployeeShort
{
    jobTitle: string;
    salary: number;
    bankName: string;
    bacAccountNumber: number;
    supervisor: number;
    site: number;
    hireDate: Date;
    fireDate: Date;
    birthDate: Date;
}