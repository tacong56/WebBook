import { HttpHeaders } from '@angular/common/http';

export class StaticVaribale {
    static URL = 'https://localhost:5001/api/';
    static URL_IMAGE = 'https://localhost:5001/';
    static PATH = {
        user: {
            login: 'users/authenticate',
            register: 'users/register',
            info: 'users/info',
            update: 'users/update',
        },
        image: {
            upload: 'image/upload',
            uploadImageProduct: 'image/upload-image-product',
            uploads: 'images/uploads',
            delete: 'image/delete'
        },
        categogry: {
            getList: 'category/getlist',
            getpaging: "category/get-paging",
            detail: "category/detail",
            create: "category/create",
            update: "category/update",
            delete: "category/delete"
        },
        product: {
            getpaging: "product/get-paging",
            delete: "product/delete",
            create: "product/create",
            update: "product/update"
        },
        account: {
            getpaging: "account/get-paging",
            delete: "account/delete",
            create: "account/create",
            update: "account/update"
        }
    }

    static httpOptions = {
        headers: new HttpHeaders({ 
          'Content-Type': 'application/json',
          'Accept': 'application/json',
        })
    };

    static httpOptionsFile = {
        headers: new HttpHeaders({ 

        })
    };
}