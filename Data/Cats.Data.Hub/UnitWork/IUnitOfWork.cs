using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Objects.DataClasses;
using Cats.Data.Repository;
using Cats.Models.Hubs;
using DRMFSS.BLL.Interfaces;


namespace Cats.Data.Hub
{

		public interface IUnitOfWork:IDisposable
        {
            #region Old Unit of work

           // IAccountRepository Account { get; set; }
                   
           //IAdjustmentRepository Adjustment { get; set; }
                   
           //IAdjustmentReasonRepository AdjustmentReason { get; set; }
                   
           //IAdminUnitRepository AdminUnit { get; set; }
                   
           //IAdminUnitTypeRepository AdminUnitType { get; set; }
                   
           //IAuditRepository Audit { get; set; }
                   
           //ICommodityRepository Commodity { get; set; }
                   
           //ICommodityGradeRepository CommodityGrade { get; set; }
                   
           //ICommoditySourceRepository CommoditySource { get; set; }
                   
           //ICommodityTypeRepository CommodityType { get; set; }
                   
           //IContactRepository Contact { get; set; }
                   
           //IDetailRepository Detail { get; set; }
                   
           //IDispatchRepository Dispatch { get; set; }
                   
           //IDispatchAllocationRepository DispatchAllocation { get; set; }
                   
           //IDispatchDetailRepository DispatchDetail { get; set; }
                   
           //IDonorRepository Donor { get; set; }
                   
           //IFDPRepository FDP { get; set; }
                   
           //IForgetPasswordRequestRepository ForgetPasswordRequest { get; set; }
                   
           //IGiftCertificateRepository GiftCertificate { get; set; }
                   
           //IGiftCertificateDetailRepository GiftCertificateDetail { get; set; }
                   
           //IHubRepository Hub { get; set; }
                   
           //IHubOwnerRepository HubOwner { get; set; }
                   
           //IHubSettingRepository HubSetting { get; set; }
                   
           //IHubSettingValueRepository HubSettingValue { get; set; }
                   
           //IInternalMovementRepository InternalMovement { get; set; }
                   
           //ILedgerRepository Ledger { get; set; }
                   
           //ILedgerTypeRepository LedgerType { get; set; }
                   
           //ILetterTemplateRepository LetterTemplate { get; set; }
                   
           //IMasterRepository Master { get; set; }
                   
           //IOtherDispatchAllocationRepository OtherDispatchAllocation { get; set; }
                   
           //IPartitionRepository Partition { get; set; }
                   
           //IPeriodRepository Period { get; set; }
                   
           //IProgramRepository Program { get; set; }
                   
           //IProjectCodeRepository ProjectCode { get; set; }
                   
           //IReceiptAllocationRepository ReceiptAllocation { get; set; }
                   
           //IReceiveRepository Receive { get; set; }
                   
           //IReceiveDetailRepository ReceiveDetail { get; set; }
                   
           //IReleaseNoteRepository ReleaseNote { get; set; }
                   
           //IRoleRepository Role { get; set; }
                   
           //ISessionAttemptRepository SessionAttempt { get; set; }
                   
           //ISessionHistoryRepository SessionHistory { get; set; }
                   
           //ISettingRepository Setting { get; set; }
                   
           //IShippingInstructionRepository ShippingInstruction { get; set; }
                   
           //ISMSRepository SMS { get; set; }
                   
           //IStackEventRepository StackEvent { get; set; }
                   
           //IStackEventTypeRepository StackEventType { get; set; }
                   
           //IStoreRepository Store { get; set; }
                   
           //ITransactionRepository Transaction { get; set; }
                   
           //ITransactionGroupRepository TransactionGroup { get; set; }
                   
           //ITranslationRepository Translation { get; set; }
                   
           //ITransporterRepository Transporter { get; set; }
                   
           //IUnitRepository Unit { get; set; }
                   
           //IUserHubRepository UserHub { get; set; }
                   
           //IUserProfileRepository UserProfile { get; set; }
                   
           //IUserRoleRepository UserRole { get; set; }
            #endregion

		    void Save();

            IGenericRepository<Account> AccountRepository { get; }

            IGenericRepository< Adjustment> AdjustmentRepository { get; }

            IGenericRepository< AdjustmentReason > AdjustmentReasonRepository{ get; }

            IGenericRepository<AdminUnit> AdminUnitRepository  { get; }

            IGenericRepository< AdminUnitType> AdminUnitTypeRepository { get; }

            IGenericRepository< Audit > AuditRepository{ get; }

            IGenericRepository< Commodity>CommodityRepository { get; }

            IGenericRepository< CommodityGrade> CommodityGradeRepository { get; }

            IGenericRepository< CommoditySource > CommoditySourceRepository{ get; }

            IGenericRepository< CommodityType> CommodityTypeRepository { get; }

            IGenericRepository< Contact > ContactRepository{ get; }

            IGenericRepository< Detail > DetailRepository{ get; }

            IGenericRepository< Dispatch> DispatchRepository { get; }

            IGenericRepository< DispatchAllocation > DispatchAllocationRepository{ get; }

            IGenericRepository<DispatchDetail > DispatchDetailRepository { get; }

            IGenericRepository<Donor > DonorRepository { get; }

            IGenericRepository< FDP> FDPRepository { get; }

            IGenericRepository< ForgetPasswordRequest >ForgetPasswordRequestRepository{ get; }

            IGenericRepository< GiftCertificate> GiftCertificateRepository { get; }

            IGenericRepository<GiftCertificateDetail >GiftCertificateDetailRepository { get; }

            IGenericRepository< Models.Hubs.Hub > HubRepository{ get; }

            IGenericRepository<HubOwner> HubOwnerRepository  { get; }

            IGenericRepository< HubSetting > HubSettingRepository{ get; }

            IGenericRepository< HubSettingValue> HubSettingValueRepository { get; }

            IGenericRepository< InternalMovement > InternalMovementRepository{ get; }

            IGenericRepository< Ledger > LedgerRepository{ get; }

            IGenericRepository< LedgerType > LedgerTypeRepository{ get; }

            IGenericRepository< LetterTemplate > LetterTemplateRepository{ get; }

            IGenericRepository< Master> MasterRepository { get; }

            IGenericRepository< OtherDispatchAllocation > OtherDispatchAllocationRepository{ get; }

            IGenericRepository<Partition>PartitionRepository  { get; }

            IGenericRepository<Period>PeriodRepository  { get; }

            IGenericRepository< Program>ProgramRepository { get; }

            IGenericRepository< ProjectCode > ProjectCodeRepository{ get; }

            IGenericRepository< ReceiptAllocation > ReceiptAllocationRepository{ get; }

            IGenericRepository< Receive >ReceiveRepository{ get; }

            IGenericRepository< ReceiveDetail > ReceiveDetailRepository{ get; }

            IGenericRepository< ReleaseNote > ReleaseNoteRepository{ get; }

            IGenericRepository< Role > RoleRepository{ get; }

            IGenericRepository< SessionAttempt > SessionAttemptRepository{ get; }

            IGenericRepository< SessionHistory > SessionHistoryRepository{ get; }

            IGenericRepository< Setting > SettingRepository{ get; }

            IGenericRepository< ShippingInstruction> ShippingInstructionRepository { get; }

            IGenericRepository< SMS> SMSRepository { get; }

            IGenericRepository< StackEvent> StackEventRepository { get; }

            IGenericRepository< StackEventType>StackEventTypeRepository { get; }

            IGenericRepository< Store > StoreRepository{ get; }

            IGenericRepository< Transaction> TransactionRepository { get; }

            IGenericRepository< TransactionGroup > TransactionGroupRepository{ get; }

            IGenericRepository< Translation > TranslationRepository{ get; }

            IGenericRepository< Transporter >TransporterRepository{ get; }

            IGenericRepository< Unit >UnitRepository{ get; }

            IGenericRepository< UserHub >UserHubRepository{ get; }

            IGenericRepository< UserProfile >UserProfileRepository{ get; }

            IGenericRepository<UserRole> UserRoleRepository { get; }
		  IReportRepository ReportRepository { get; }
		    IGenericRepository<ErrorLog> ErrorLogRepository { get; }
          
        }
}

