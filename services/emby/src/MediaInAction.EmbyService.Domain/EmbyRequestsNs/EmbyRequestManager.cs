﻿using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace MediaInAction.EmbyService.EmbyRequestsNs
{
    public class EmbyRequestManager : DomainService
    {
        private readonly IEmbyRequestRepository _embyRequestRepository;

        public EmbyRequestManager(IEmbyRequestRepository requestRepository)
        {
            _embyRequestRepository = requestRepository;
        }

        public EmbyRequest2 CreateAsync(
          //  EmbyOperation operation,
            //List<EmbyRequestItem> files
            )
        {
            /*
            var existingEmbyEntry = await _fileRequestRepository.FindByServerNameAsync(
                p => p.Server == server &&
                p.Embyname == filename && p.Directory == directory);
            if (existingEmbyEntry != null)
            {
                throw new EmbyEntryAlreadyExistsException(filename);
            }
            

            return await _fileRequestRepository.InsertAsync(
                new EmbyRequest(
                    GuidGenerator.Create(),
                    operation
                )
            );
            */
            return  null;
        }
    }
}
