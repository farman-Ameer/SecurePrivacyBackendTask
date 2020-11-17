using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MedicineShopBackEnd.Data.Helpers;
using MedicineShopBackEnd.Data.IRepository;
using MedicineShopBackEnd.Model;
using MedicineShopBackEnd.Model.DTO;
using MedicineShopBackEnd.Model.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MedicineShopBackEnd.Data.Repository
{
   public class OrdersRepository : UnitOfWork<OrdersModel>, IOrdersRepository
    {
        private readonly MedicineShopDbContext _context;
        private IConfiguration _configuration;
        private IHttpContextAccessor _httpContextAccessor;
        private IUserRepository userRepository;
        private IProductRepository productRepository;
        //private readonly IUrlHelper _urlHelper;
        public OrdersRepository(MedicineShopDbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IUserRepository _userRepository, IProductRepository _productRepository) : base(context, configuration, httpContextAccessor)
        {
            this._context = context;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            userRepository = _userRepository;
            productRepository = _productRepository;
            //_urlHelper = urlHelper;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveDeliveryAddress(DeliveryInfoDTO deliveryInfoDTO)
        {
            DeliveryInfoModel deliveryInfoModel = new DeliveryInfoModel();
            try
            {
                deliveryInfoModel.PersonName = deliveryInfoDTO.PersonName;
                deliveryInfoModel.PostalCode = deliveryInfoDTO.PostalCode;
                deliveryInfoModel.State = deliveryInfoDTO.State;
                deliveryInfoModel.ContactNumber = deliveryInfoDTO.ContactNumber;
                deliveryInfoModel.Address = deliveryInfoDTO.Address;
                _context.DeliveryInfo.Add(deliveryInfoModel);
                await _context.SaveChangesAsync();
                return deliveryInfoModel.Id;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        public async Task<int> SaveOrder(OrderShipmentDTO order)
        {
            OrdersModel neworder = new OrdersModel();
            try
            {
                neworder.allProductNote = order.allProductNote;
                neworder.totalAmount = order.total;
                neworder.subTotal = (decimal)order.subTotal;
                neworder.deliveryCharges = order.deliveryCharges;
                neworder.deliveryInstructions = order.deliveryInstructions;
                neworder.insertedOn = DateTime.Now;
                neworder.orderStatus = (int)Constant.OrderStatus.Pending;
                neworder.userID = order.userID;
                neworder.address = order.address.street + "," + order.address.appartment + "," + order.address.city + "," + order.address.state + "," + order.address.zipCode;
                _context.Orders.Add(neworder);
                await _context.SaveChangesAsync();
                return neworder.OrderId;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public async Task SaveOrderLines(int orderId,List<OrderLineDTO> Products)
        {
            try
            {
                List<OrderDetailsModel> list = new List<OrderDetailsModel>();
                OrderDetailsModel orderDetails = new OrderDetailsModel();
                string note = "";
                foreach (var item in Products)
                {
                    list.Add(new OrderDetailsModel
                    {
                        addedOn = DateTime.Now,
                        OrderId = orderId,
                        price = await productRepository.GetProductPrice(item.productId),
                        ProdQnty = item.quantity,
                        ProductId = item.productId
                    });
                }
                _context.OrderDetails.AddRange(list);
                await _context.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public PagedList<OrderDTO> GetOrders(int ? userId = 0, int? pageNumber = 1, int? pageSize = 10)
        {
            PagingParamsDTO pagingParams = new PagingParamsDTO();
            pagingParams.PageNumber = pageNumber;
            pagingParams.PageSize = pageSize;
            IQueryable<OrderDTO> query;
            try
            {
                //var query = userId == 0 || userId == null ? _context.Orders : _context.Orders.Where(x => x.userID == userId);
                if(userId == 0 || userId == null)
                {
                    query = _context.Orders.Select(s=> new OrderDTO {
                        OrderId = s.OrderId,
                        address = s.address,
                        deliveryCharges = s.deliveryCharges,
                        weightScale = s.weightScale,
                        orderStatus = s.orderStatus,
                        shopperId = s.shopperId,
                        subTotal = s.subTotal,
                        totalAmount = s.totalAmount,
                        totalTax = s.totalTax,
                        insertedOn = s.insertedOn
                    });
                }
                else
                {
                    query = from a in Context.Orders
                            join c in Context.Users
                            on a.userID equals c.userId
                            where a.userID == userId
                            select new OrderDTO
                            {
                                OrderId = a.OrderId,
                                address = a.address,
                                deliveryCharges = a.deliveryCharges,
                                weightScale = a.weightScale,
                                orderStatus = a.orderStatus,
                                shopperId = a.shopperId,
                                subTotal = a.subTotal,
                                totalAmount = a.totalAmount,
                                totalTax = a.totalTax,
                                insertedOn = a.insertedOn,
                                orderByUser = c.firstName + " " + c.lastName
                            };
                }
                    return new PagedList<OrderDTO>(
                       query, pagingParams.PageNumber, pagingParams.PageSize);
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
        public async Task<OrderDetailsDTO> GetOrdersDetails(int orderId)
        {
            try
            {
                var data = await (from a in Context.Orders
                                      // join b in Context.OrderDetails
                                      // on a.OrderId equals b.OrderId
                                  join c in Context.Users
                                  on a.userID equals c.userId
                                  where a.OrderId == orderId
                                  select new OrderDetailsDTO
                                  {
                                      order = new OrderDTO
                                      {
                                          OrderId = a.OrderId,
                                          address = a.address,
                                          deliveryCharges = a.deliveryCharges,
                                          weightScale = a.weightScale,
                                          orderStatus = a.orderStatus,
                                          shopperId = a.shopperId,
                                          subTotal = a.subTotal,
                                          totalAmount = a.totalAmount,
                                          totalTax = a.totalTax,
                                          insertedOn = a.insertedOn,
                                          orderLines =  GetOrderLines(orderId)
                                      },
                                      user =  new UserDTO  {
                                          firstName = c.firstName,
                                          lastName = c.lastName,
                                          userAddress = c.userAddress,
                                          userEmail = c.userEmail,
                                          userId = c.userId,
                                          userPhone = c.userPhone,
                                          userProfileImage = c.userProfileImage,
                                          userZipCode = c.userZipCode,
                                          //userRoles = await userRepository.GetUserRoleByUserId(c.userId).
                                      },
                                  }).FirstOrDefaultAsync();
                return data;
            } 
            catch (Exception exce)
            {
                throw exce;
            }
        }
        public List<OrderLineDTO> GetOrderLines(int orderId)
        {
            try
            {
                var data = (from a in Context.OrderDetails
                                  join b in Context.Products
                                  on a.ProductId equals b.productId
                                  join c in Context.Categories
                                  on b.categoryId equals c.categoryId
                                  where a.OrderId == orderId
                                  select new OrderLineDTO
                                  {
                                      category = c.categoryName,
                                      categoryId = c.categoryId,
                                      productBrand = b.productBrand,
                                      productDesc = b.productDesc,
                                      productId = b.productId,
                                      productImage = b.productImage,
                                      productName = b.productName,
                                      productPrice = b.productPrice,
                                      productWeight = b.productWeight,
                                      quantity = a.ProdQnty,
                                  }).ToList();
                return data;
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }
    
    }
}
