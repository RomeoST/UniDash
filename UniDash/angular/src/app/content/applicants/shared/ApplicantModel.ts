export interface IApplicantModel{
    applicantId : number,
    nameApplicant : string,
    mailApplicant : string,
    phoneApplicant : string,
    schoolCollege : string,
    address : string,
    speciality : string,
    specialityList : number[],
    mark : string,
    nameFound : string,
    nameAdded : string,
    dateEdit : string,
    dateAdd : string,
    selected : boolean
}

export class ApplicantModel implements IApplicantModel{
    applicantId: number;    nameApplicant: string;
    mailApplicant: string;
    phoneApplicant: string;
    schoolCollege: string;
    address: string;
    speciality: string;
    specialityList: number[];
    mark: string;
    nameFound: string;
    nameAdded: string;
    dateEdit: string;
    dateAdd: string;
    selected: boolean;

    constructor(id : number, name){
        this.applicantId = id;
        this.nameApplicant = name;
        this.mailApplicant = '';
        this.phoneApplicant = '';
        this.schoolCollege = '';
        this.address = '';
        this.speciality = '';
        this.specialityList = [];
        this.mark = '';
        this.nameFound = '';
        this.nameAdded = '';
        this.dateEdit = '';
        this.dateAdd = '';
        this.selected = false;
    }

    static newApplicant(){
        let newAppl = new ApplicantModel(0, "Новий абітурієнт");
        newAppl.selected = true;
        return newAppl;
    }
}