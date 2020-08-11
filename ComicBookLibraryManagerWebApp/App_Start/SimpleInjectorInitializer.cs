[assembly: WebActivator.PostApplicationStartMethod(typeof(ComicBookLibraryManagerWebApp.App_Start.SimpleInjectorInitializer), "Initialize")]

namespace ComicBookLibraryManagerWebApp.App_Start
{
    using System.Reflection;
    using System.Web;
    using System.Web.Mvc;
    using ComicBookShared.Data;
    using ComicBookShared.Models;
    using ComicBookShared.Security;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Microsoft.Owin;
    using SimpleInjector;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
    
    public static class SimpleInjectorInitializer
    {
        /// <summary>Initialize the container and register it as MVC3 Dependency Resolver.</summary>
        public static void Initialize()
        {
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            
            InitializeContainer(container);

            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            
            container.Verify();
            
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
     
        private static void InitializeContainer(Container container)
        {
            container.Register<Context>(Lifestyle.Scoped);

            container.Register<BaseComicBooksRepository, ComicBooksRepository>(Lifestyle.Scoped);
            container.Register<BaseSeriesRepository, SeriesRepository>(Lifestyle.Scoped);
            container.Register<BaseComicBookArtistsRepository, ComicBookArtistsRepository>(Lifestyle.Scoped);
            container.Register<BaseArtistsRepository, ArtistsRepository>(Lifestyle.Scoped);
            container.Register<BaseUserRepository, UserRepository>(Lifestyle.Scoped);

            container.Register<ComicBookArtistsRepository>(Lifestyle.Scoped);
            container.Register<SeriesRepository>(Lifestyle.Scoped);

            container.Register<ApplicationUserManager>(Lifestyle.Scoped);
            container.Register<ApplicationSignInManager>(Lifestyle.Scoped);

            container.Register(() =>
            container.IsVerifying
                ? new OwinContext().Authentication
                : HttpContext.Current.GetOwinContext().Authentication,
            Lifestyle.Scoped);

            container.Register<IUserStore<User>>(() =>
                        new UserStore<User>(container.GetInstance<Context>()),
                        Lifestyle.Scoped);
        }
    }
}