import { Injectable } from '@angular/core';
import { SystemConstants } from '@core/system.constants';

@Injectable({
  providedIn: 'root',
})
export class DateTimeService {
  public getLocalISOTime = (date: Date): string => {
    const timezoneOffset = date.getTimezoneOffset() * 60 * 1000;
    const localISOTime = new Date(
      date.getTime() - timezoneOffset
    ).toISOString();
    const result = localISOTime.substring(0, localISOTime.length - 1);
    return result;
  }

  public getNZLocaleDateString(date: Date) {
    const dateString = date.toLocaleString(SystemConstants.locale, {
      timeZone: SystemConstants.nzTimeZone,
    });

    return dateString;
  }

  public getUtcDateString(dateString: string) {
    const date = new Date(dateString);
    const nzDate = this.getNZLocaleDateString(date);
    return new Date(nzDate).toUTCString();
  }

  public getNZLocalISOTime = (dateString: string): string => {
    const date = new Date(dateString);
    const aestTime = this.getNZLocaleDateString(date);
    return this.getLocalISOTime(new Date(aestTime));
  }
}
