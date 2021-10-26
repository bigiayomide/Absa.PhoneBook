import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class HandleErrorInterceptor implements HttpInterceptor {
  constructor(private toasterService: ToastrService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return new Observable((observer)=>{

        next.handle(req).subscribe(
            (res:HttpResponse<any>)=>{
                if(res instanceof HttpResponse){
                    observer.next(res);
                }
            },
            (err: HttpErrorResponse)=>{
                this.handleError(err);
            }
        )
    });

  }
    handleError(err: HttpErrorResponse){
        let errorMessage: string;
        if(err.error instanceof ErrorEvent)
        {
            errorMessage = `An error occurred: ${err.error.message}`
        }
        else{
            console.log(err.message);
            errorMessage = 'Something went wrong'
        }

        this.toasterService.error(errorMessage);
    }
}
