import { HttpHeaders } from '@angular/common/http';

export class StaticVaribale {
    static URL = 'https://localhost:5001/api/';
    static URL_IMAGE = 'https://localhost:5001/';
    static PATH = {
        user: {
            login: 'users/authenticate',
            loginClient: 'users/authenticate-client',
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
            getListnoauth: 'category/getlist-no-auth',
            getpaging: "category/get-paging",
            detail: "category/detail",
            create: "category/create",
            update: "category/update",
            delete: "category/delete"
        },
        product: {
            getpaging: "product/get-paging",
            getpaging2: "product/get-paging2",
            getbyparentcategory: "product/get-by-parent-category",
            getpagingnoauth: "product/get-paging-no-auth",
            getlist: "product/get-list",
            getall: "product/get-all",
            delete: "product/delete",
            create: "product/create",
            update: "product/update"
        },
        account: {
            getpaging: "account/get-paging",
            delete: "account/delete",
            create: "account/create",
            update: "account/update",
            get: 'account/get',
            changepass: "account/change-password",
            lock: "account/lock"
        },
        order: {
            getpaging: "order/get-paging",
            getpagingtran: "order/get-paging-tran",
            create: 'order/create',
            updateStatus: 'order/update-status',
            createVNPay: 'order/CreateOrderVNPay',
            get: 'order/detail',
            getlist: 'order/getlist',
            dorder: 'order/get-d-order',
            dpie: 'order/get-pie-order',
            topproduct: 'order/top-product'
        },
        importStorage: {
            getpaging: "importstorage/get-paging",
            get: "importstorage/detail",
            create: "importstorage/create",
            update: "importstorage/update"
        },
        exportStorage: {
            getpaging: "exportstorage/get-paging",
            get: "exportstorage/detail",
            create: "exportstorage/create",
            update: "exportstorage/update"
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