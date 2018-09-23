export class ApplicantModel{
    constructor(
        public ApplicantId : string = "",
        public ApplicantName : string = "",
        public ApplicantMail : string = "",
        public ApplicantPhone : string = "",
        public SchoolCollege : string = "",
        public Address : string = "",
        public Speciality : number[] = [],
        public Mark : string = "",
        public NameFound : string = "",
        public NameAdded : string = "",
        public DateEdit : string = "",
        public DateAdd : string = "",
        public selected : boolean = false
    ){}
}