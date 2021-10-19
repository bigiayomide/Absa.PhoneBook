import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';

export interface MyPagination {
  itemsCount: number;
  pageSize: number;
}
@Component({
  selector: 'app-pagination',
  templateUrl: './app-pagination.component.html',
  styleUrls: ['./app-pagination.component.scss']
})
export class AppPaginationComponent {
  public pagesArray: Array<number> = [];
  public currentPage: number = 1;
  @Output() goToPage = new EventEmitter<number>();
  @Input() set setPagination(pagination: MyPagination) {
    if (pagination) {
      const pagesAmount = Math.ceil(
        pagination.itemsCount / pagination.pageSize
      );
      this.pagesArray = new Array(pagesAmount).fill(1);
    }
  }
  public setPage(pageNumber: number): void {
    if (pageNumber === this.currentPage)
      return;
    this.currentPage = pageNumber;
    this.goToPage.emit(pageNumber);
  }
}