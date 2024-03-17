export class EventResponseDto {
  public id: number;
  public institutionName: string;
  public institutionAddress: string;
  public numberOfStudents: number;
  public startTime: Date;
  public servicePrice: number;
  public email: string;
  public creationDate?: Date;
  public updateDate?: Date;

  constructor(
    id: number,
    institutionName: string,
    institutionAddress: string,
    numberOfStudents: number,
    startTime: Date,
    servicePrice: number,
    email: string,
    creationDate?: Date,
    updateDate?: Date
  ) {
    this.id = id;
    this.institutionName = institutionName;
    this.institutionAddress = institutionAddress;
    this.numberOfStudents = numberOfStudents;
    this.startTime = startTime;
    this.servicePrice = servicePrice;
    this.email = email;
    this.creationDate = creationDate;
    this.updateDate = updateDate;
  }
}
