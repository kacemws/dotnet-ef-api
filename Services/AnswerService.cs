﻿using System;
using System.Threading.Tasks;

namespace API_2
{
    public class AnswerService : CRUDService<Answer> , IAnswerService
    {
        IUnitOfWork _unitOfWork;
        private readonly IAnswerRepository _answerRepository;

        public AnswerService(IUnitOfWork unitOfWork, IAnswerRepository answerRepository) : base(unitOfWork, answerRepository)
        {
            _unitOfWork = unitOfWork;
            _answerRepository = answerRepository;
        }
    }
}
