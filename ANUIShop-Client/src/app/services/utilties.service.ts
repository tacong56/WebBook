import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})

export class UtiltiesService {
    constructor() {}

    /**
     * 
     * @param dateString 
     * @param fomatter
     * type fomat:
     * dd/mm/yyyy,
     * dd/mm/yyyy hh:MM,
     * ...
     * @returns datetime string
     */
    public converDate(dateString: string, fomatter: string): string {
        const unix_timestamp = Date.parse(dateString);
        const date = new Date(unix_timestamp);
        // Hours part from the timestamp
        const dates = date.getDate();
        // Hours part from the timestamp
        const month = date.getMonth() + 1;
        // Hours part from the timestamp
        const years = date.getFullYear();
        // Hours part from the timestamp
        const hours = date.getHours();
        // Minutes part from the timestamp
        const minutes = "0" + date.getMinutes();
        // Seconds part from the timestamp
        const seconds = "0" + date.getSeconds();

        let output: string;
        switch(fomatter) {
            case "dd/mm/yyyy": {
                output = `${dates}/${month}/${years}`;
                break;
            }
            case "dd/mm/yyyy hh:MM": {
                output = `${dates}/${month}/${years} ${hours}:${minutes}`;
                break;
            }
            case "dd/mm/yyyy hh:MM:ss": {
                output = `${dates}/${month}/${years} ${hours}:${minutes}:${seconds}`;
                break;
            }
            case "dd-mm-yyyy": {
                output = `${dates}-${month}-${years}`;
                break;
            }
        }

        return output;
    }

    public converDateTime(date: any, fomatter: string): string {
        // Hours part from the timestamp
        const dates = date.getDate();
        // Hours part from the timestamp
        const month = date.getMonth() + 1;
        // Hours part from the timestamp
        const years = date.getFullYear();
        // Hours part from the timestamp
        const hours = date.getHours();
        // Minutes part from the timestamp
        const minutes = date.getMinutes();
        // Seconds part from the timestamp
        const seconds = date.getSeconds();

        let output: string;
        switch(fomatter) {
            case "dd/mm/yyyy": {
                output = `${dates}/${month}/${years}`;
                break;
            }
            case "dd/mm/yyyy hh:MM": {
                output = `${dates}/${month}/${years} ${hours}:${minutes}`;
                break;
            }
            case "dd/mm/yyyy hh:MM:ss": {
                output = `${dates}/${month}/${years} ${hours}:${minutes}:${seconds}`;
                break;
            }
            case "yyyy/mm/dd": {
                output = `${years}/${month}/${dates}`;
                break;
            }
            case "yyyy-mm-dd": {
                output = `${years}-${month}-${dates}`;
                break;
            }
            case "dd-mm-yyyy": {
                output = `${dates}-${month}-${years}`;
                break;
            }
        }

        return output;
    }
}