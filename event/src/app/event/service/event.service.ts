import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, Subject, tap } from 'rxjs';
import { Constants } from 'src/app/shared/util/constants';
import { EventResponseDto } from '../dtos/event-response-dto';
import { EventRequestDto } from '../dtos/event-request-dto';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private URL: string = Constants.API_URL.URL;
  private httpClient: HttpClient;
  public changeSubject = new Subject<boolean>();

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  listEvent(): Observable<EventResponseDto[]> {
    return this.httpClient.get<EventResponseDto[]>(
      `${this.URL}Event`
    );
  }

  getEvent(id:number): Observable<EventResponseDto> {
    return this.httpClient.get<EventResponseDto>(
      `${this.URL}Event/`+id
    );
  }

  saveEvent( dto:EventRequestDto): Observable<any> {
    return this.httpClient.post<any>(
      `${this.URL}Event`,dto
    ).pipe(  tap(() => this.changeSubject.next(true)));
  }

  updateEvent(id: number, dto:EventRequestDto): Observable<any> {
    return this.httpClient.put<any>(
      `${this.URL}Event/`+id,dto
    ).pipe(  tap(() => this.changeSubject.next(true)));
  }

  deleteEvent(id: number): Observable<void> {
    return this.httpClient.delete<void>( `${this.URL}Event/` + id).pipe(  tap(() => this.changeSubject.next(true)));
  }

}
