import { Component, OnInit } from '@angular/core';
import { PhoneBookClient, PhoneBookEntryClient, PhoneBookEntryDto } from '../web-api-client';

@Component({
  selector: 'app-phonebook',
  templateUrl: './phonebook.component.html',
  styleUrls: ['./phonebook.component.scss']
})

export class PhonebookComponent implements OnInit {
  public phonebookEntry: Array<PhoneBookEntryDto>;
  public totalphonebook: number = 0;
  private _currentPage: number = 1;
  private _currentSearchValue: string = '';
  constructor(
    private _myService: PhoneBookEntryClient
  ) { }
  ngOnInit() {
    this._loadPhoneBook(
      this._currentPage,
      this._currentSearchValue
    );
  }
  public filterList(searchParam: string): void {
    this._currentSearchValue = searchParam;
    this._loadPhoneBook(
      this._currentPage,
      this._currentSearchValue
    );
  }
  public goToPage(page: number): void {
    this._currentPage = page;
    this._loadPhoneBook(
      this._currentPage,
      this._currentSearchValue
    );
  }

  private _loadPhoneBook(
    page: number = 1, searchParam: string = '' 
  ) {
    this._myService.getTodoItemsWithPagination(
      1,page,searchParam, 10
    ).subscribe((response) => {
      response.items
      this.phonebookEntry = response.items;
      this.totalphonebook = response.totalCount;
    }, (error) => console.error(error));
  }
}
