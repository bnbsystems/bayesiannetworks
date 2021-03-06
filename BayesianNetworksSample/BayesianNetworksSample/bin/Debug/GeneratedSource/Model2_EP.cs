using System;
using MicrosoftResearch.Infer;
using MicrosoftResearch.Infer.Distributions;
using MicrosoftResearch.Infer.Collections;
using MicrosoftResearch.Infer.Factors;

namespace MicrosoftResearch.Infer.Models.User
{
	/// <summary>
	/// Class for performing inference in model 'Model' using algorithm 'ExpectationPropagation'.
	/// </summary>
	/// <remarks>
	/// The easiest way to use this class is to wrap an instance in a CompiledAlgorithm object and use
	/// the methods on CompiledAlgorithm to set parameters and execute inference.
	/// 
	/// If you instead wish to use this class directly, you must perform the following steps:
	/// 1) Create an instance of the class
	/// 2) Set the value of any externally-set fields e.g. data, priors
	/// 3) Call the Execute(numberOfIterations) method
	/// 4) Use the XXXMarginal() methods to retrieve posterior marginals for different variables.
	/// 
	/// Generated by Infer.NET 2.4 beta 3 at 01:55 on 11 stycznia 2012.
	/// </remarks>
	public class Model2_EP : IGeneratedAlgorithm
	{
		#region Fields
		/// <summary>Field backing the NumberOfIterationsDone property</summary>
		private int numberOfIterationsDone;
		/// <summary>Field backing the Cloudy property</summary>
		private bool cloudy;
		/// <summary>Field backing the WetGrass property</summary>
		private bool wetGrass;
		/// <summary>Field backing the Sprinkler property</summary>
		private bool sprinkler;
		/// <summary>The number of iterations last computed by Changed_Cloudy_WetGrass_Sprinkler. Set this to zero to force re-execution of Changed_Cloudy_WetGrass_Sprinkler</summary>
		public int Changed_Cloudy_WetGrass_Sprinkler_iterationsDone;
		public Bernoulli Cloudy_marginal;
		public Bernoulli WetGrass_marginal;
		public Bernoulli Sprinkler_marginal;
		/// <summary>Message to marginal of 'Rain'</summary>
		public Bernoulli Rain_marginal_F;
		public Bernoulli Rain_cond_Sprinkler_0_selector_cases_0_B;
		public DistributionStructArray<Bernoulli,bool> Rain_cond_Sprinkler_0_selector_cases_B;
		public Bernoulli Rain_cond_Sprinkler_0_selector_cases_1_B;
		public Bernoulli Rain_cond_Sprinkler_0_selector_B;
		public Bernoulli Rain_cond_Sprinkler_1_selector_cases_0_B;
		public DistributionStructArray<Bernoulli,bool> Rain_cond_Sprinkler_1_selector_cases_B;
		public Bernoulli Rain_cond_Sprinkler_1_selector_cases_1_B;
		public Bernoulli Rain_cond_Sprinkler_1_selector_B;
		#endregion

		#region Properties
		/// <summary>The number of iterations done from the initial state</summary>
		public int NumberOfIterationsDone
		{			get {
				return this.numberOfIterationsDone;
			}
		}

		/// <summary>The externally-specified value of 'Cloudy'</summary>
		public bool Cloudy
		{			get {
				return this.cloudy;
			}
			set {
				if (this.cloudy!=value) {
					this.cloudy = value;
					this.numberOfIterationsDone = 0;
					this.Changed_Cloudy_WetGrass_Sprinkler_iterationsDone = 0;
				}
			}
		}

		/// <summary>The externally-specified value of 'WetGrass'</summary>
		public bool WetGrass
		{			get {
				return this.wetGrass;
			}
			set {
				if (this.wetGrass!=value) {
					this.wetGrass = value;
					this.numberOfIterationsDone = 0;
					this.Changed_Cloudy_WetGrass_Sprinkler_iterationsDone = 0;
				}
			}
		}

		/// <summary>The externally-specified value of 'Sprinkler'</summary>
		public bool Sprinkler
		{			get {
				return this.sprinkler;
			}
			set {
				if (this.sprinkler!=value) {
					this.sprinkler = value;
					this.numberOfIterationsDone = 0;
					this.Changed_Cloudy_WetGrass_Sprinkler_iterationsDone = 0;
				}
			}
		}

		#endregion

		#region Methods
		/// <summary>Get the observed value of the specified variable.</summary>
		/// <param name="variableName">Variable name</param>
		public object GetObservedValue(string variableName)
		{
			if (variableName=="Cloudy") {
				return this.Cloudy;
			}
			if (variableName=="WetGrass") {
				return this.WetGrass;
			}
			if (variableName=="Sprinkler") {
				return this.Sprinkler;
			}
			throw new ArgumentException("Not an observed variable name: "+variableName);
		}

		/// <summary>Set the observed value of the specified variable.</summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="value">Observed value</param>
		public void SetObservedValue(string variableName, object value)
		{
			if (variableName=="Cloudy") {
				this.Cloudy = (bool)value;
				return ;
			}
			if (variableName=="WetGrass") {
				this.WetGrass = (bool)value;
				return ;
			}
			if (variableName=="Sprinkler") {
				this.Sprinkler = (bool)value;
				return ;
			}
			throw new ArgumentException("Not an observed variable name: "+variableName);
		}

		/// <summary>The marginal distribution of the specified variable.</summary>
		/// <param name="variableName">Variable name</param>
		public object Marginal(string variableName)
		{
			if (variableName=="Cloudy") {
				return this.CloudyMarginal();
			}
			if (variableName=="Rain") {
				return this.RainMarginal();
			}
			if (variableName=="Sprinkler") {
				return this.SprinklerMarginal();
			}
			if (variableName=="WetGrass") {
				return this.WetGrassMarginal();
			}
			throw new ArgumentException("This class was not built to infer "+variableName);
		}

		public T Marginal<T>(string variableName)
		{
			return Distribution.ChangeType<T>(this.Marginal(variableName));
		}

		/// <summary>The query-specific marginal distribution of the specified variable.</summary>
		/// <param name="variableName">Variable name</param>
		/// <param name="query">QueryType name. For example, GibbsSampling answers 'Marginal', 'Samples', and 'Conditionals' queries</param>
		public object Marginal(string variableName, string query)
		{
			if (query=="Marginal") {
				return this.Marginal(variableName);
			}
			throw new ArgumentException(((("This class was not built to infer \'"+variableName)+"\' with query \'")+query)+"\'");
		}

		public T Marginal<T>(string variableName, string query)
		{
			return Distribution.ChangeType<T>(this.Marginal(variableName, query));
		}

		/// <summary>The output message of the specified variable.</summary>
		/// <param name="variableName">Variable name</param>
		public object GetOutputMessage(string variableName)
		{
			throw new ArgumentException("This class was not built to compute an output message for "+variableName);
		}

		/// <summary>Update all marginals, by iterating message passing the given number of times</summary>
		/// <param name="numberOfIterations">The number of times to iterate each loop</param>
		/// <param name="initialise">If true, messages that initialise loops are reset when observed values change</param>
		private void Execute(int numberOfIterations, bool initialise)
		{
			this.Changed_Cloudy_WetGrass_Sprinkler();
			this.numberOfIterationsDone = numberOfIterations;
		}

		public void Execute(int numberOfIterations)
		{
			this.Execute(numberOfIterations, true);
		}

		public void Update(int additionalIterations)
		{
			this.Execute(this.numberOfIterationsDone+additionalIterations, false);
		}

		private void OnProgressChanged(ProgressChangedEventArgs e)
		{
			// Make a temporary copy of the event to avoid a race condition
			// if the last subscriber unsubscribes immediately after the null check and before the event is raised.
			EventHandler<ProgressChangedEventArgs> handler = this.ProgressChanged;
			if (handler!=null) {
				handler(this, e);
			}
		}

		/// <summary>Reset all messages to their initial values.  Sets NumberOfIterationsDone to 0.</summary>
		public void Reset()
		{
			this.Execute(0);
		}

		/// <summary>Computations that depend on the observed value of Cloudy and WetGrass and Sprinkler</summary>
		public void Changed_Cloudy_WetGrass_Sprinkler()
		{
			if (this.Changed_Cloudy_WetGrass_Sprinkler_iterationsDone==1) {
				return ;
			}
			// The constant 'vBernoulli0'
			Bernoulli vBernoulli0 = Bernoulli.FromLogOdds(0);
			this.Cloudy_marginal = ArrayHelper.MakeUniform<Bernoulli>(vBernoulli0);
			this.Cloudy_marginal = Distribution.SetPoint<Bernoulli,bool>(this.Cloudy_marginal, this.cloudy);
			// The constant 'vBernoulli5'
			Bernoulli vBernoulli5 = Bernoulli.FromLogOdds(4.5951198501345889);
			this.WetGrass_marginal = ArrayHelper.MakeUniform<Bernoulli>(vBernoulli5);
			this.WetGrass_marginal = Distribution.SetPoint<Bernoulli,bool>(this.WetGrass_marginal, this.wetGrass);
			// The constant 'vBernoulli1'
			Bernoulli vBernoulli1 = Bernoulli.FromLogOdds(-2.1972245773362191);
			this.Sprinkler_marginal = ArrayHelper.MakeUniform<Bernoulli>(vBernoulli1);
			this.Sprinkler_marginal = Distribution.SetPoint<Bernoulli,bool>(this.Sprinkler_marginal, this.sprinkler);
			// The constant 'vBernoulli3'
			Bernoulli vBernoulli3 = Bernoulli.FromLogOdds(1.3862943611198908);
			Bernoulli Rain_F = ArrayHelper.MakeUniform<Bernoulli>(vBernoulli3);
			this.Rain_marginal_F = ArrayHelper.MakeUniform<Bernoulli>(vBernoulli3);
			// Message from use of 'Rain'
			Bernoulli Rain_use_B = ArrayHelper.MakeUniform<Bernoulli>(vBernoulli3);
			if (this.cloudy) {
				// Message to 'Rain' from Copy factor
				Rain_F = Factor.Copy<Bernoulli>(vBernoulli3);
			}
			// The constant 'vBernoulli4'
			Bernoulli vBernoulli4 = Bernoulli.FromLogOdds(-1.3862943611198906);
			if (!this.cloudy) {
				// Message to 'Rain' from Copy factor
				Rain_F = Factor.Copy<Bernoulli>(vBernoulli4);
			}
			// Rain_F is now updated in all contexts
			if (this.sprinkler) {
				this.Rain_cond_Sprinkler_0_selector_cases_0_B = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
				// Message to 'Rain_cond_Sprinkler_0_selector_cases_0' from Random factor
				this.Rain_cond_Sprinkler_0_selector_cases_0_B = Bernoulli.FromLogOdds(UnaryOp<bool>.LogEvidenceRatio<Bernoulli>(this.wetGrass, vBernoulli5));
				// Create array for 'Rain_cond_Sprinkler_0_selector_cases' Backwards messages.
				this.Rain_cond_Sprinkler_0_selector_cases_B = new DistributionStructArray<Bernoulli,bool>(2);
				for(int _ind0 = 0; _ind0<2; _ind0++) {
					this.Rain_cond_Sprinkler_0_selector_cases_B[_ind0] = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
				}
				// Message to 'Rain_cond_Sprinkler_0_selector_cases' from Copy factor
				this.Rain_cond_Sprinkler_0_selector_cases_B[0] = Factor.Copy<Bernoulli>(this.Rain_cond_Sprinkler_0_selector_cases_0_B);
			}
			// The constant 'vBernoulli6'
			Bernoulli vBernoulli6 = Bernoulli.FromLogOdds(2.1972245773362196);
			if (this.sprinkler) {
				this.Rain_cond_Sprinkler_0_selector_cases_1_B = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
				// Message to 'Rain_cond_Sprinkler_0_selector_cases_1' from Random factor
				this.Rain_cond_Sprinkler_0_selector_cases_1_B = Bernoulli.FromLogOdds(UnaryOp<bool>.LogEvidenceRatio<Bernoulli>(this.wetGrass, vBernoulli6));
				// Message to 'Rain_cond_Sprinkler_0_selector_cases' from Copy factor
				this.Rain_cond_Sprinkler_0_selector_cases_B[1] = Factor.Copy<Bernoulli>(this.Rain_cond_Sprinkler_0_selector_cases_1_B);
				this.Rain_cond_Sprinkler_0_selector_B = ArrayHelper.MakeUniform<Bernoulli>(vBernoulli3);
				// Message to 'Rain_cond_Sprinkler_0_selector' from Cases factor
				this.Rain_cond_Sprinkler_0_selector_B = CasesOp.BAverageConditional(this.Rain_cond_Sprinkler_0_selector_cases_B);
				// Message to 'Rain_use' from Copy factor
				Rain_use_B = Factor.Copy<Bernoulli>(this.Rain_cond_Sprinkler_0_selector_B);
			}
			if (!this.sprinkler) {
				this.Rain_cond_Sprinkler_1_selector_cases_0_B = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
				// Message to 'Rain_cond_Sprinkler_1_selector_cases_0' from Random factor
				this.Rain_cond_Sprinkler_1_selector_cases_0_B = Bernoulli.FromLogOdds(UnaryOp<bool>.LogEvidenceRatio<Bernoulli>(this.wetGrass, vBernoulli6));
				// Create array for 'Rain_cond_Sprinkler_1_selector_cases' Backwards messages.
				this.Rain_cond_Sprinkler_1_selector_cases_B = new DistributionStructArray<Bernoulli,bool>(2);
				for(int _ind0 = 0; _ind0<2; _ind0++) {
					this.Rain_cond_Sprinkler_1_selector_cases_B[_ind0] = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
				}
				// Message to 'Rain_cond_Sprinkler_1_selector_cases' from Copy factor
				this.Rain_cond_Sprinkler_1_selector_cases_B[0] = Factor.Copy<Bernoulli>(this.Rain_cond_Sprinkler_1_selector_cases_0_B);
			}
			// The constant 'vBernoulli8'
			Bernoulli vBernoulli8 = Bernoulli.FromLogOdds(Double.NegativeInfinity);
			if (!this.sprinkler) {
				this.Rain_cond_Sprinkler_1_selector_cases_1_B = ArrayHelper.MakeUniform<Bernoulli>(new Bernoulli());
				// Message to 'Rain_cond_Sprinkler_1_selector_cases_1' from Random factor
				this.Rain_cond_Sprinkler_1_selector_cases_1_B = Bernoulli.FromLogOdds(UnaryOp<bool>.LogEvidenceRatio<Bernoulli>(this.wetGrass, vBernoulli8));
				// Message to 'Rain_cond_Sprinkler_1_selector_cases' from Copy factor
				this.Rain_cond_Sprinkler_1_selector_cases_B[1] = Factor.Copy<Bernoulli>(this.Rain_cond_Sprinkler_1_selector_cases_1_B);
				this.Rain_cond_Sprinkler_1_selector_B = ArrayHelper.MakeUniform<Bernoulli>(vBernoulli3);
				// Message to 'Rain_cond_Sprinkler_1_selector' from Cases factor
				this.Rain_cond_Sprinkler_1_selector_B = CasesOp.BAverageConditional(this.Rain_cond_Sprinkler_1_selector_cases_B);
				// Message to 'Rain_use' from Copy factor
				Rain_use_B = Factor.Copy<Bernoulli>(this.Rain_cond_Sprinkler_1_selector_B);
			}
			// Rain_use_B is now updated in all contexts
			if (this.cloudy) {
				// Message to 'Rain_marginal' from DerivedVariable factor
				this.Rain_marginal_F = DerivedVariableOp.MarginalAverageConditional<Bernoulli>(Rain_use_B, Rain_F, this.Rain_marginal_F);
			}
			if (!this.cloudy) {
				// Message to 'Rain_marginal' from DerivedVariable factor
				this.Rain_marginal_F = DerivedVariableOp.MarginalAverageConditional<Bernoulli>(Rain_use_B, vBernoulli4, this.Rain_marginal_F);
			}
			// Rain_marginal_F is now updated in all contexts
			this.Changed_Cloudy_WetGrass_Sprinkler_iterationsDone = 1;
		}

		/// <summary>
		/// Returns the marginal distribution for 'Cloudy' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public Bernoulli CloudyMarginal()
		{
			return this.Cloudy_marginal;
		}

		/// <summary>
		/// Returns the marginal distribution for 'Rain' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public Bernoulli RainMarginal()
		{
			return this.Rain_marginal_F;
		}

		/// <summary>
		/// Returns the marginal distribution for 'Sprinkler' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public Bernoulli SprinklerMarginal()
		{
			return this.Sprinkler_marginal;
		}

		/// <summary>
		/// Returns the marginal distribution for 'WetGrass' given by the current state of the
		/// message passing algorithm.
		/// </summary>
		/// <returns>The marginal distribution</returns>
		public Bernoulli WetGrassMarginal()
		{
			return this.WetGrass_marginal;
		}

		#endregion

		#region Events
		/// <summary>Event that is fired when the progress of inference changes, typically at the end of one iteration of the inference algorithm.</summary>
		public event EventHandler<ProgressChangedEventArgs> ProgressChanged;
		#endregion

	}

}
