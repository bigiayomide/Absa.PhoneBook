import { Component, OnInit, TemplateRef } from '@angular/core';
import { faEllipsisH, faPlus } from '@fortawesome/free-solid-svg-icons';
import { BsModalRef, BsModalService } from 'ngx-bootstrap';
import { CreatePhoneBookCommand, CreatePhoneBookEntryCommand, PhoneBookClient, 
  PhoneBookDto, PhoneBookEntryClient, PhoneBookEntryDto, 
  PhoneBookVm, UpdatePhoneBookCommand, UpdatePhoneBookEntryCommand } from '../web-api-client';

@Component({
  selector: 'app-phonebook',
  templateUrl: './phonebook.component.html',
  styleUrls: ['./phonebook.component.scss']
})

// export class PhonebookComponent implements OnInit {
//   public phonebookEntry: Array<PhoneBookEntryDto>;
//   public totalphonebook: number = 0;
//   private _currentPage: number = 1;
//   private _currentSearchValue: string = '';
//   constructor(
//     private _myService: PhoneBookEntryClient
//   ) { }
//   ngOnInit() {
//     this._loadPhoneBook(
//       this._currentPage,
//       this._currentSearchValue
//     );
//   }
//   public filterList(searchParam: string): void {
//     this._currentSearchValue = searchParam;
//     this._loadPhoneBook(
//       this._currentPage,
//       this._currentSearchValue
//     );
//   }
//   public goToPage(page: number): void {
//     this._currentPage = page;
//     this._loadPhoneBook(
//       this._currentPage,
//       this._currentSearchValue
//     );
//   }

//   private _loadPhoneBook(
//     page: number = 1, searchParam: string = '' 
//   ) {
//     this._myService.getTodoItemsWithPagination(
//       3,page,searchParam, 10
//     ).subscribe((response) => {
//       this.phonebookEntry = response.items;
//       this.totalphonebook = response.totalCount;
//       console.log(response);
//     }, (error) => console.error(error));
//   }
// }

export class PhonebookComponent {

    debug = false;

    vm: PhoneBookVm;

    selectedList: PhoneBookDto;
    selectedItem: PhoneBookEntryDto;

    newListEditor: any = {};
    listOptionsEditor: any = {};
    itemDetailsEditor: any = {};

    newListModalRef: BsModalRef;
    listOptionsModalRef: BsModalRef;
    deleteListModalRef: BsModalRef;
    itemDetailsModalRef: BsModalRef;

    faPlus = faPlus;
    faEllipsisH = faEllipsisH;

    constructor(private listsClient: PhoneBookClient, private itemsClient: PhoneBookEntryClient, private modalService: BsModalService) {
        listsClient.get().subscribe(
            result => {
                this.vm = result;
                if (this.vm.phoneBooks.length) {
                    this.selectedList = this.vm.phoneBooks[0];
                }
            },
            error => console.error(error)
        );
    }

    showNewListModal(template: TemplateRef<any>): void {
        this.newListModalRef = this.modalService.show(template);
        setTimeout(() => document.getElementById("name").focus(), 250);
    }

    newListCancelled(): void {
        this.newListModalRef.hide();
        this.newListEditor = {};
    }

    addList(): void {
        let list = PhoneBookDto.fromJS({
            id: 0,
            name: this.newListEditor.name,
            items: [],
        });

        this.listsClient.create(<CreatePhoneBookCommand>{ name: this.newListEditor.name }).subscribe(
            result => {
                list.id = result;
                this.vm.phoneBooks.push(list);
                this.selectedList = list;
                this.newListModalRef.hide();
                this.newListEditor = {};
            },
            error => {
                let errors = JSON.parse(error.response);

                if (errors && errors.Title) {
                    this.newListEditor.error = errors.Title[0];
                }

                setTimeout(() => document.getElementById("name").focus(), 250);
            }
        );
    }

    showListOptionsModal(template: TemplateRef<any>) {
        this.listOptionsEditor = {
            id: this.selectedList.id,
            name: this.selectedList.name,
        };

        this.listOptionsModalRef = this.modalService.show(template);
    }

    updateListOptions() {
        this.listsClient.update(this.selectedList.id, UpdatePhoneBookCommand.fromJS(this.listOptionsEditor))
            .subscribe(
                () => {
                    this.selectedList.name = this.listOptionsEditor.name,
                    this.listOptionsModalRef.hide();
                    this.listOptionsEditor = {};
                },
                error => console.error(error)
            );
    }

    confirmDeleteList(template: TemplateRef<any>) {
        this.listOptionsModalRef.hide();
        this.deleteListModalRef = this.modalService.show(template);
    }

    totalEntries(list: PhoneBookDto): number {
        return list.phoneBookEntries?.length;
    }

    deleteListConfirmed(): void {
        this.listsClient.delete(this.selectedList.id).subscribe(
            () => {
                this.deleteListModalRef.hide();
                this.vm.phoneBooks = this.vm.phoneBooks.filter(t => t.id != this.selectedList.id)
                this.selectedList = this.vm.phoneBooks.length ? this.vm.phoneBooks[0] : null;
            },
            error => console.error(error)
        );
    }

    // Items

    showItemDetailsModal(template: TemplateRef<any>, item: PhoneBookEntryDto): void {
        this.selectedItem = item;
        this.itemDetailsEditor = {
            ...this.selectedItem
        };

        this.itemDetailsModalRef = this.modalService.show(template);
    }


    updatePhoneBookEntries(): void {
        this.itemsClient.update(this.selectedItem.id, UpdatePhoneBookEntryCommand.fromJS(this.itemDetailsEditor))
            .subscribe(
                () => {
                    if (this.selectedItem.phoneBookId != this.itemDetailsEditor.listId) {
                        this.selectedList.phoneBookEntries = this.selectedList.phoneBookEntries.filter(i => i.id != this.selectedItem.id)
                        let listIndex = this.vm.phoneBooks.findIndex(l => l.id == this.itemDetailsEditor.phoneBookId);
                        this.selectedItem.phoneBookId = this.itemDetailsEditor.phoneBookId;
                        this.vm.phoneBooks[listIndex].phoneBookEntries.push(this.selectedItem);
                        debugger;
                    }

                    this.itemDetailsModalRef.hide();
                    this.itemDetailsEditor = {};
                },
                error => console.error(error)
            );
    }

    addItem() {
        let item = PhoneBookEntryDto.fromJS({
            id: 0,
            phoneBookId: this.selectedList.id,
            name: '',
            number:''
        });

        this.selectedList.phoneBookEntries.push(item);
        let index = this.selectedList.phoneBookEntries.length - 1;
        this.editItem(item, 'itemTitle' + index);
    }

    editItem(item: PhoneBookEntryDto, inputId: string): void {
        this.selectedItem = item;
        setTimeout(() => document.getElementById(inputId).focus(), 100);
    }

    updateItem(item: PhoneBookEntryDto, pressedEnter: boolean = false): void {
        let isNewItem = item.id == 0;

        if (!item.name.trim()) {
            this.deleteItem(item);
            return;
        }

        if (item.id == 0) {
            this.itemsClient.create(CreatePhoneBookEntryCommand.fromJS({ ...item, phoneBookId: this.selectedList.id }))
                .subscribe(
                    result => {
                        item.id = result;
                    },
                    error => console.error(error)
                );
        } else {
            this.itemsClient.update(item.id, UpdatePhoneBookEntryCommand.fromJS(item))
                .subscribe(
                    () => console.log('Update succeeded.'),
                    error => console.error(error)
                );
        }

        this.selectedItem = null;

        if (isNewItem && pressedEnter) {
            this.addItem();
        }
    }

    // Delete item
    deleteItem(item: PhoneBookEntryDto) {
        if (this.itemDetailsModalRef) {
            this.itemDetailsModalRef.hide();
        }

        if (item.id == 0) {
            let itemIndex = this.selectedList.phoneBookEntries.indexOf(this.selectedItem);
            this.selectedList.phoneBookEntries.splice(itemIndex, 1);
        } else {
            this.itemsClient.delete(item.id).subscribe(
                () => this.selectedList.phoneBookEntries = this.selectedList.phoneBookEntries.filter(t => t.id != item.id),
                error => console.error(error)
            );
        }
    }
}
