import dataProviderUser from './DataProviderUsers';
import dataProviderRule1 from './DataProviderRule1';

export default {
    getList: (resource, params) => {

        switch(resource) {
            case 'Users':
                return dataProviderUser.getList(resource, params);

            case 'Rule1':
                return dataProviderRule1.getList(resource, params);
            
            default:
                return "";
          }
    },

    getOne: (resource, params) => {

        switch(resource) {
            case 'Users':
                return dataProviderUser.getOne(resource, params);

            case 'Rule1':
                return dataProviderRule1.getOne(resource, params);
            
            default:
                return "";
          }
    },

    getMany: (resource, params) => {

        switch(resource) {
            case 'Users':
                return dataProviderUser.getMany(resource, params);

            case 'Rule1':
                return dataProviderRule1.getMany(resource, params);
            
            default:
                return "";
          }
    },

    getManyReference: (resource, params) => {

        switch(resource) {
            case 'Users':
                return dataProviderUser.getManyReference(resource, params);

            case 'Rule1':
                return dataProviderRule1.getManyReference(resource, params);
            
            default:
                return "";
          }
    },

    update: (resource, params) => {

        switch(resource) {
            case 'Users':
                return dataProviderUser.update(resource, params);

            case 'Rule1':
                return dataProviderRule1.update(resource, params);
            
            default:
                return "";
          }
    },

    updateMany: (resource, params) => {

        switch(resource) {
            case 'Users':
                return dataProviderUser.updateMany(resource, params);

            case 'Rule1':
                return dataProviderRule1.updateMany(resource, params);
            
            default:
                return "";
          }
    },

    create: (resource, params) => {

        switch(resource) {
            case 'Users':
                return dataProviderUser.create(resource, params);

            case 'Rule1':
                return dataProviderRule1.create(resource, params);
            
            default:
                return "";
          }
    },

    delete: (resource, params) => {

        switch(resource) {
            case 'Users':
                return dataProviderUser.delete(resource, params);

            case 'Rule1':
                return dataProviderRule1.delete(resource, params);
            
            default:
                return "";
          }
    },

    deleteMany: (resource, params) => {

        switch(resource) {
            case 'Users':
                return dataProviderUser.deleteMany(resource, params);

            case 'Rule1':
                return dataProviderRule1.deleteMany(resource, params);
            
            default:
                return "";
          }
    }
}
